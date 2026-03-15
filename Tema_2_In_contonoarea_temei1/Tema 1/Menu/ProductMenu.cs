using System;
using System.Linq;
using System.Collections.Generic;
using Tema_1.Catalog;
using Tema_1.Core;

namespace Tema_1.Menu;

public class ProductMenu
{
    private readonly Store _store;
    private readonly InputService _input;

    public ProductMenu(Store store, InputService input)
    {
        _store = store;
        _input = input;
    }

    public void ShowProducts()
    {
        var products = _store.Catalog.GetAllProducts();
        PrintProducts(products);
    }

    public void FilterByCategory()
    {
        var category = SelectCategory();

        if (category == null)
            return;

        var filter = new ProductFilter
        {
            Category = category
        };

        var results = _store.SearchService.SearchProducts(filter);
        PrintProducts(results);
    }

    public void SortProducts()
    {
        Console.WriteLine("\nSorting options:");
        Console.WriteLine("1 - Price ascending");
        Console.WriteLine("2 - Price descending");
        Console.WriteLine("3 - Name A-Z");

        var choice = _input.ReadString("Option:");

        var filter = new ProductFilter();

        switch (choice)
        {
            case "1":
                filter.OrderBy = ProductOrderBy.Price;
                break;

            case "2":
                filter.OrderBy = ProductOrderBy.Price;
                filter.Descending = true;
                break;

            case "3":
                filter.OrderBy = ProductOrderBy.Name;
                break;

            default:
                Console.WriteLine("Invalid option");
                return;
        }

        var results = _store.SearchService.SearchProducts(filter);
        PrintProducts(results);
    }

    public void FilterByPriceRange()
    {
        decimal min = ReadDecimal("Minimum price: ");
        decimal max = ReadDecimal("Maximum price: ");

        var filter = new ProductFilter
        {
            MinPrice = min,
            MaxPrice = max
        };

        var results = _store.SearchService.SearchProducts(filter);
        PrintProducts(results);
    }

    public void ShowGroupedProducts()
    {
        var groups = _store.SearchService.GetProductsGrouped();

        foreach (var group in groups)
        {
            Console.WriteLine($"\n{group.Key}");

            foreach (var p in group)
                Console.WriteLine($"{p.Id} - {p.Name} - {p.Price}");
        }
    }

    private string? SelectCategory()
    {
        var products = _store.Catalog.GetAllProducts();

        var categories = products
            .Select(p => p.Category)
            .Distinct()
            .ToList();

        Console.WriteLine("\nAvailable categories:");

        for (int i = 0; i < categories.Count; i++)
            Console.WriteLine($"{i + 1} - {categories[i]}");

        int choice = _input.ReadInt("Choose category:");

        if (choice < 1 || choice > categories.Count)
        {
            Console.WriteLine("Invalid selection");
            return null;
        }

        return categories[choice - 1];
    }

    private void PrintProducts(IEnumerable<Product> products)
    {
        Console.WriteLine("\nProducts:");

        foreach (var p in products)
            Console.WriteLine($"{p.Id} - {p.Name} - {p.Category} - {p.Price} RON");
    }

    private decimal ReadDecimal(string message)
    {
        Console.Write(message);

        if (!decimal.TryParse(Console.ReadLine(), out decimal value))
        {
            Console.WriteLine("Invalid number");
            return 0;
        }

        return value;
    }
}