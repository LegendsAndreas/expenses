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
    
    public async Task<List<Models.Expenses>?> GetExpenses()
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
    
    public async Task<List<Models.Expenses>?> GetDailyExpenses(DateOnly date)
    {
        string localStorageExpenses = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", $"daily-expense-{date.ToString("dd-MM-yyyy")}");
        
        if (string.IsNullOrEmpty(localStorageExpenses))
        {
            Console.WriteLine("No expenses found in local storage");
            return null;
        }

        List<Models.Expenses>? expenses = null;
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
    
    public async Task UpdateExpenses(List<Models.Expenses> expenses)
    {
        Console.WriteLine("Updating expenses");
        
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "expenses", JsonSerializer.Serialize(expenses));
        await _jsRuntime.InvokeVoidAsync("alert", "Expenses updated successfully");
    }
    
    public async Task UpdateAllExpenses(List<Models.Expenses> expenses, List<Models.Expenses>? dailyExpenses)
    {
        Console.WriteLine("Updating expenses");
        
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "expenses", JsonSerializer.Serialize(expenses));

        if (dailyExpenses != null)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", $"daily-expense-{dailyExpenses.First().Name}", JsonSerializer.Serialize(dailyExpenses));
        }
        
        await _jsRuntime.InvokeVoidAsync("alert", "Expenses updated successfully");
    }
    
    public async Task ExportExpenses()
    {
        await _jsRuntime.InvokeVoidAsync("exportExpenses");
    }
    
    public async Task ExportDailyExpenses(DateOnly date)
    {
        await _jsRuntime.InvokeVoidAsync("exportDailyExpenses", date.ToString("dd-MM-yyyy"));
    }
    
    public async Task ImportExpenses()
    {
        await _jsRuntime.InvokeVoidAsync("importExpenses");
    }
    
    public async Task<bool> ImportDailyExpenses(DateOnly date)
    {
        return await _jsRuntime.InvokeAsync<bool>("importDailyExpenses", date.ToString("dd-MM-yyyy"));
    }
}