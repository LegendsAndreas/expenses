using System.ComponentModel.DataAnnotations;

namespace Expenses.Models;

public class Expenses
{
    [Required]
    public string Name { get; set; } = "";
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
    public string Name { get; set; } = "";
    public float Price { get; set; } = 0;
}