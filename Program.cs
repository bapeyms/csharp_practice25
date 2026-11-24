using System;
using System.Globalization;
using System.Reflection;
namespace csharp_practice25;

enum CurrencyFormat
{
    Hryvna,
    Dollar,
    Euro
}

interface IProduct
{
    string Name { get; set; }
    decimal Price { get; set; }
}

[AttributeUsage(AttributeTargets.Class)]
class PriceFormatAttribute : Attribute
{
    public CurrencyFormat Format { get; }
    public PriceFormatAttribute(CurrencyFormat format)
    {
        Format = format;
    }
}

[PriceFormat(CurrencyFormat.Hryvna)]
class FoodProduct : IProduct
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public FoodProduct(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}

[PriceFormat(CurrencyFormat.Euro)]
class ElectronProduct : IProduct
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ElectronProduct(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}

class OtherProduct : IProduct
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public OtherProduct(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}

class ProductPrinter
{
    public void Show(IProduct product)
    {
        PriceFormatAttribute attr = product.GetType().GetCustomAttribute<PriceFormatAttribute>();
        string priceText;
        
        if (attr == null)
            priceText = product.Price.ToString("F2", CultureInfo.InvariantCulture);
        else
        {
            string number = product.Price.ToString("F2", CultureInfo.InvariantCulture);
            switch (attr.Format)
            {
                case CurrencyFormat.Hryvna:
                    priceText = number + " hrn";
                    break;
                case CurrencyFormat.Dollar:
                    priceText = number + " dol";
                    break;
                case CurrencyFormat.Euro:
                    priceText = number + " euro";
                    break;
                default:
                    priceText = number;
                    break;
            }
        }
        Console.WriteLine($"Product {product.Name} ({priceText})");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ProductPrinter printer = new ProductPrinter();
        IProduct[] products =
        {
            new FoodProduct("Bread", 25.5m),
            new ElectronProduct("Headphones", 49.99m),
            new OtherProduct("Bag", 10m)
        };
        foreach (IProduct product in products)
            printer.Show(product);
    }
}