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

    public string PrintPrice()
    {
        switch (Currency)
        {
            case "DKK":
                return SumPrice(1).ToString(CultureInfo.CurrentCulture) + ",-kr";
            case "USD":
                return SumPrice(6.4f).ToString(CultureInfo.CurrentCulture) + ",-$";
            case "EUR":
                return SumPrice(7.5f).ToString(CultureInfo.CurrentCulture) + ",-€";
            default:
                throw new ArgumentException("Unsupported currency");
        }
    }

    public float SumPrice(float multiplier)
    {
        float sum = 0;
        foreach (var item in Items)
        {
            if (item.Currency == "DKK")
            {
                sum += item.Price * 1;
            }
            else if (item.Currency == "USD")
            {
                sum += item.Price * 6.4f;
            }
            else if (item.Currency == "EUR")
            {
                sum += item.Price * 7.5f;
            }
        }

        return sum / multiplier;
    }
}

public class ExpensesItem
{
    [Required] public string Name { get; set; } = "";
    [Required] public string Currency { get; set; } = "";

    [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public float Price { get; set; }
}