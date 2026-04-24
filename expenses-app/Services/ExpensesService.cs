using System.Text.Json;
using Microsoft.JSInterop;

namespace Expenses.Services;

public class ExpensesService
{
    private IJSRuntime _jsRuntime;
    public ExpensesService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    public async Task<List<Models.Expenses>> GetExpenses()
    {
        string localStorageExpenses = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "expenses");
        
        if (string.IsNullOrEmpty(localStorageExpenses))
        {
            Console.WriteLine("No expenses found in local storage");
            return [];
        }

        List<Models.Expenses> expenses = [];
        try
        {
            expenses = JsonSerializer.Deserialize<List<Models.Expenses>>(localStorageExpenses);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return expenses;
    }
    
    public async Task UpdateAllExpenses(List<Models.Expenses> expenses)
    {
        Console.WriteLine("Updating expenses");
        // Print all expenses
        foreach (var expense in expenses)
        {
            expense.PrintExpenses();
        }
        
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "expenses", JsonSerializer.Serialize(expenses));
    }
    
    public async Task ExportExpenses()
    {
        await _jsRuntime.InvokeVoidAsync("exportExpenses");
    }
    
    public async Task ImportExpenses()
    {
        await _jsRuntime.InvokeVoidAsync("importExpenses");
    }
}