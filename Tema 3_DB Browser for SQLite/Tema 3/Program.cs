using Microsoft.EntityFrameworkCore;
using Tema_3.Cart;
using Tema_3.Data;
using Tema_3.Orders;
using Tema3.Products;

var connectionString = "Server=(localdb)\\mssqllocaldb;Database=OnlineShoppingDb;Trusted_Connection=True;";

var options = new DbContextOptionsBuilder<ShoppingDbContext>()
    .UseSqlite("Data Source=shopping.db")
    .Options;

var context = new ShoppingDbContext(options);
await context.Database.MigrateAsync();

var productRepository = new ProductRepository(context);
var cartRepository = new CartRepository(context);
var orderRepository = new OrderRepository(context);

var productService = new ProductService(productRepository);
var cartService = new CartService(cartRepository, productRepository);
var orderService = new OrderService(orderRepository, cartRepository, productRepository, context);

await SeedDataAsync(productService);
await RunAppAsync(productService, cartService, orderService);

async Task SeedDataAsync(ProductService productService)
{
    var products = await productService.GetAllProductsAsync();
    if (products.Any()) return;

    await productService.AddProductAsync("Laptop", 3999.99m, 10, "Electronics");
    await productService.AddProductAsync("Phone", 1999.99m, 20, "Electronics");
    await productService.AddProductAsync("Desk", 899.99m, 5, "Furniture");
    await productService.AddProductAsync("Chair", 499.99m, 8, "Furniture");

    Console.WriteLine("Products added successfully!");
}

async Task RunAppAsync(ProductService productService, CartService cartService, OrderService orderService)
{
    var running = true;

    while (running)
    {
        Console.WriteLine("\n=== Online Shopping ===");
        Console.WriteLine("1. View all products");
        Console.WriteLine("2. Search products");
        Console.WriteLine("3. Add to cart");
        Console.WriteLine("4. View cart");
        Console.WriteLine("5. Remove from cart");
        Console.WriteLine("6. Checkout");
        Console.WriteLine("7. View orders");
        Console.WriteLine("0. Exit");
        Console.Write("\nChoice: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await ShowProductsAsync(productService);
                break;
            case "2":
                await SearchProductsAsync(productService);
                break;
            case "3":
                await AddToCartAsync(productService, cartService);
                break;
            case "4":
                await ShowCartAsync(cartService);
                break;
            case "5":
                await RemoveFromCartAsync(cartService);
                break;
            case "6":
                await CheckoutAsync(cartService, orderService);
                break;
            case "7":
                await ShowOrdersAsync(orderService);
                break;
            case "0":
                running = false;
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}

async Task ShowProductsAsync(ProductService productService)
{
    var products = await productService.GetAllProductsAsync();
    Console.WriteLine("\n=== Products ===");
    foreach (var p in products)
        Console.WriteLine($"[{p.Id}] {p}");
}

async Task SearchProductsAsync(ProductService productService)
{
    Console.Write("Search: ");
    var name = Console.ReadLine() ?? "";
    var products = await productService.SearchProductsAsync(name);
    Console.WriteLine("\n=== Results ===");
    foreach (var p in products)
        Console.WriteLine($"[{p.Id}] {p}");
}

async Task AddToCartAsync(ProductService productService, CartService cartService)
{
    await ShowProductsAsync(productService);
    Console.Write("Product ID: ");
    if (!int.TryParse(Console.ReadLine(), out var productId)) return;

    Console.Write("Quantity: ");
    if (!int.TryParse(Console.ReadLine(), out var quantity)) return;

    await cartService.AddToCartAsync(productId, quantity);
    Console.WriteLine("Added to cart!");
}

async Task ShowCartAsync(CartService cartService)
{
    var cart = await cartService.GetCartAsync();
    Console.WriteLine("\n=== Cart ===");
    if (cart.IsEmpty())
    {
        Console.WriteLine("Cart is empty.");
        return;
    }
    foreach (var item in cart.Items)
        Console.WriteLine($"{item.Product.Name} x{item.Quantity} = {item.GetTotal():C}");
    Console.WriteLine($"Total: {cart.GetTotal():C}");
}

async Task RemoveFromCartAsync(CartService cartService)
{
    Console.Write("Product ID to remove: ");
    if (!int.TryParse(Console.ReadLine(), out var productId)) return;
    await cartService.RemoveFromCartAsync(productId);
    Console.WriteLine("Removed from cart!");
}

async Task CheckoutAsync(CartService cartService, OrderService orderService)
{
    var cart = await cartService.GetCartAsync();
    if (cart.IsEmpty())
    {
        Console.WriteLine("Cart is empty!");
        return;
    }

    var order = await orderService.CheckoutAsync(cart.Id);
    Console.WriteLine($"\nOrder placed successfully!");
    Console.WriteLine(order.ToString());
}

async Task ShowOrdersAsync(OrderService orderService)
{
    var orders = await orderService.GetAllOrdersAsync();
    Console.WriteLine("\n=== Orders ===");
    foreach (var o in orders)
        Console.WriteLine(o.ToString());
}