using System.ComponentModel.DataAnnotations;

namespace Expenses.Models;

public class Expenses
{
    [Required]
    public string Name { get; set; } = "";
    [ValidateComplexType]
    public List<ExpensesItem> Items { get; set; } = [];
    
    public void PrintExpenses()
    {
        Console.WriteLine($"Name: {Name}");
        foreach (var item in Items)
        {
            Console.WriteLine($"{item.Name}: {item.Price}");
        }
    }
}

public class ExpensesItem
{
    [Required]
    public string Name { get; set; } = "";
    [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public float Price { get; set; }
}