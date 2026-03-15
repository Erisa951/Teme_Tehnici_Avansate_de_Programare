using Tema_2.Catalog;
using Tema_2.Domain;
using Tema_2.Services;

var catalog = new ProductCatalog();

SeedProducts(catalog);

var searchService = new ProductSearchService(catalog);

RunMenu(searchService);

static void RunMenu(ProductSearchService service)
{
    while (true)
    {
        Console.WriteLine("\n==== PRODUCT MENU ====");
        Console.WriteLine("1 - Show all products");
        Console.WriteLine("2 - Filter by category");
        Console.WriteLine("3 - Sort products");
        Console.WriteLine("4 - Filter by price range");
        Console.WriteLine("5 - Group products by category");
        Console.WriteLine("0 - Exit");

        Console.Write("Choose option: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                ShowAllProducts(service);
                break;

            case "2":
                FilterByCategory(service);
                break;

            case "3":
                SortProducts(service);
                break;

            case "4":
                FilterByPriceRange(service);
                break;

            case "5":
                GroupProducts(service);
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Invalid option");
                break;
        }
    }
}

static void SeedProducts(ProductCatalog catalog)
{
    catalog.Add(new Product(1, "Laptop", "Electronics", 3500));
    catalog.Add(new Product(2, "Mouse", "Electronics", 100));
    catalog.Add(new Product(3, "Keyboard", "Electronics", 200));
    catalog.Add(new Product(4, "Desk", "Furniture", 800));
    catalog.Add(new Product(5, "Chair", "Furniture", 400));
    catalog.Add(new Product(6, "Monitor", "Electronics", 1200));
    catalog.Add(new Product(7, "Lamp", "Furniture", 150));
}

static void ShowAllProducts(ProductSearchService service)
{
    var products = service.SearchProducts(new ProductFilter());
    PrintProducts(products);
}

static void FilterByCategory(ProductSearchService service)
{
    var category = SelectCategory(service);

    if (category == null)
        return;

    var filter = new ProductFilter
    {
        Category = category
    };

    var results = service.SearchProducts(filter);

    PrintProducts(results);
}

static string? SelectCategory(ProductSearchService service)
{
    var products = service.SearchProducts(new ProductFilter());

    var categories = products
        .Select(p => p.Category)
        .Distinct()
        .ToList();

    Console.WriteLine("\nAvailable categories:");

    for (int i = 0; i < categories.Count; i++)
        Console.WriteLine($"{i + 1} - {categories[i]}");

    Console.Write("Choose category: ");

    if (!int.TryParse(Console.ReadLine(), out int choice) ||
        choice < 1 || choice > categories.Count)
    {
        Console.WriteLine("Invalid selection");
        return null;
    }

    return categories[choice - 1];
}

static void SortProducts(ProductSearchService service)
{
    Console.WriteLine("\nSorting options:");
    Console.WriteLine("1 - Price ascending");
    Console.WriteLine("2 - Price descending");
    Console.WriteLine("3 - Name A-Z");

    var choice = Console.ReadLine();

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

    var results = service.SearchProducts(filter);

    PrintProducts(results);
}

static void FilterByPriceRange(ProductSearchService service)
{
    var min = ReadDecimal("Minimum price: ");
    var max = ReadDecimal("Maximum price: ");

    var filter = new ProductFilter
    {
        MinPrice = min,
        MaxPrice = max
    };

    var results = service.SearchProducts(filter);

    PrintProducts(results);
}

static decimal ReadDecimal(string message)
{
    Console.Write(message);

    if (!decimal.TryParse(Console.ReadLine(), out decimal value))
    {
        Console.WriteLine("Invalid number");
        return 0;
    }

    return value;
}

static void GroupProducts(ProductSearchService service)
{
    var groups = service.GetProductsGrouped();

    foreach (var group in groups)
    {
        Console.WriteLine($"\n{group.Key}");

        foreach (var p in group)
            Console.WriteLine($"   {p.Name} | {p.Price}");
    }
}

static void PrintProducts(IEnumerable<Product> products)
{
    Console.WriteLine("\nProducts:");

    foreach (var p in products)
        Console.WriteLine($"{p.Name} | {p.Category} | {p.Price}");
}