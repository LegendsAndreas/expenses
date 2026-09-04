using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Expenses.Models;

public class Expenses
{
    [Required] public string Name { get; set; } = "";
    public string Currency { get; set; } = "DKK";
    [ValidateComplexType] public List<ExpensesItem> Items { get; set; } = [];

    public void PrintExpenses()
    {
        Console.WriteLine($"Name: {Name}");
        foreach (var item in Items)
        {
            Console.WriteLine($"{item.Name}: {item.Price}");
        }
    }

    public float GetMonthlyPrice()
    {
        switch (Currency)
        {
            case "DKK":
                return SumPrice(1);
            case "USD":
                return SumPrice(6.4f);
            case "EUR":
                return SumPrice(7.5f);
            default:
                throw new ArgumentException("Unsupported currency");
        }
    }

    /**
     * Note that if currency string is not set for an item, nothing happens for that item, which is actually fine
     * for when adding a new item in the frontend.
     */
    public float SumPrice(float multiplier)
    {
        float sum = 0;
        foreach (var item in Items)
        {
            if (item.Currency == "DKK")
            {
                sum += item.GetItemTotalPrice();
            }
            else if (item.Currency == "USD")
            {
                sum += item.GetItemTotalPrice();
            }
            else if (item.Currency == "EUR")
            {
                sum += item.GetItemTotalPrice();
            }
        }

        return sum / multiplier;
    }
    
    public string GetCurrencySymbol()
    {
        switch (Currency)
        {
            case "DKK":
                return ",-kr";
            case "USD":
                return ",-$";
            case "EUR":
                return ",-€";
            default:
                Console.WriteLine("No currency set, or unsupported currency");
                return "";
        }
    }

    public void PrintEverything()
    {
        Console.WriteLine("|----------------Printing everything----------------|");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Currency: {Currency}");
        foreach (var item in Items)
        {
            Console.WriteLine($"{item.Name}: {item.Price}");
        }
    }
}

public class ExpensesItem
{
    [Required] public string Name { get; set; } = "";
    [Required] public string Currency { get; set; } = "";
    [Required] 
    [Range(1, 31)]
    public int CountedDaysInAMonth { get; set; } = 1;

    [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public float Price { get; set; }
    
    public float GetItemTotalPrice()
    {
        return Price * CountedDaysInAMonth * GetCurrencyMultiplier();
    }

    public float GetCurrencyMultiplier()
    {
        return Currency switch
        {
            "DKK" => 1,
            "USD" => 6.4f,
            "EUR" => 7.5f,
            _ => 1
        };
    }
    
    public string GetCurrencySymbol()
    {
        switch (Currency)
        {
            case "DKK":
                return ",-kr";
            case "USD":
                return ",-$";
            case "EUR":
                return ",-€";
            default:
                Console.WriteLine("No currency set, or unsupported currency");
                return "";
        }
    }

    public void PrintItem()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Currency: {Currency}");
        Console.WriteLine($"Price: {Price}");
        Console.WriteLine($"Counted days in a month: {CountedDaysInAMonth}");
        Console.WriteLine($"Item total price: {GetItemTotalPrice()}");
    }
}