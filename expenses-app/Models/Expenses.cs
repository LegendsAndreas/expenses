using System.ComponentModel.DataAnnotations;

namespace Expenses.Models;

public class Expenses
{
    [Required]
    public string Name { get; set; } = "";
    public Dictionary<string, float> Items { get; set; } = [];
    
    public void PrintExpenses()
    {
        Console.WriteLine($"Name: {Name}");
        foreach (var item in Items)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}