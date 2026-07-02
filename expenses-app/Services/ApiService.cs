using System.Net.Http.Json;
using Expenses.Models;

namespace Expenses.Services;

public partial class ApiService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<(string msg, Recipe recipe)> GetRecipe (int id)
    {
        try
        {
            Recipe? recipe = await _httpClient.GetFromJsonAsync<Recipe>($"recipes/{id}");

            if (recipe == null)
            {
                return ("Recipe not found", new Recipe());
            }
            
            Console.WriteLine($"{recipe.Cost}, {recipe.Name}, {recipe.Id}");
            
            return ("", recipe);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ("Error getting recipe: " + e.Message, new Recipe());
        }
    }

    public async Task<(string msg, List<RecipeSearchItemDto> recipes)> GetRecipeSearchResults(string searchTerm)
    {
        try
        {
            List<RecipeSearchItemDto>? recipes = await _httpClient.GetFromJsonAsync<List<RecipeSearchItemDto>>($"recipes/realtime-search/{searchTerm}");

            if (recipes == null)
            {
                return ("No recipes found", new List<RecipeSearchItemDto>());
            }
            
            return ("", recipes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ("Error getting recipe: " + e.Message, []);
        }
    }
}
