namespace Expenses.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public float Cost { get; set; }
}

public class RecipeSearchItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}