using System;
using System.Linq;
using Tema_1.Catalog;
using Tema_1.Core;
using Tema_1.Discounts;
using Tema_1.Order;
using Tema_1.Users.Customer;
using Tema_1.Menu;

namespace Tema_1.Menu;

public class CustomerMenu
{
    private readonly Store _store;
    private readonly OrderService _orderService;
    private readonly InputService _input;

    private readonly ProductMenu _productMenu;
    private readonly CartMenu _cartMenu;
    private readonly CheckoutMenu _checkoutMenu;

    public CustomerMenu(Store store, OrderService orderService, InputService input)
    {
        _store = store;
        _orderService = orderService;
        _input = input;

        _productMenu = new ProductMenu(store, input);
        _cartMenu = new CartMenu(store, input);
        _checkoutMenu = new CheckoutMenu(store, orderService, input);
    }

    public void Run()
    {
        var cart = new ShoppingCart(new NoDiscount());
        var customer = new Customer(2, "Customer", cart);

        while (true)
        {
            try
            {
                if (HandleCustomerAction(customer))
                    break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private bool HandleCustomerAction(Customer customer)
    {
        Console.WriteLine("\n==== PRODUCT MENU ====");
        Console.WriteLine("1 - Show all products");
        Console.WriteLine("2 - Filter by category");
        Console.WriteLine("3 - Sort products");
        Console.WriteLine("4 - Filter by price range");
        Console.WriteLine("5 - Group products by category");
        Console.WriteLine("0 - View cart");

        int option = _input.ReadInt("Option:");

        switch (option)
        {
            case 1:
                _productMenu.ShowProducts();
                break;

            case 2:
                _productMenu.FilterByCategory();
                break;

            case 3:
                _productMenu.SortProducts();
                break;

            case 4:
                _productMenu.FilterByPriceRange();
                break;

            case 5:
                _productMenu.ShowGroupedProducts();
                break;

            case 0:
                _cartMenu.ShowCart(customer);

                if (AskContinueShopping())
                    return false;

                _checkoutMenu.ChooseShippingAndPayment(customer);
                return true;
        }

        Console.WriteLine("\nEnter product ID:");
        int id = _input.ReadInt("Product:");

        AddProductToCart(customer, id);

        return false;
    }

    private void AddProductToCart(Customer customer, int productId)
    {
        var product = _store.Catalog
            .GetAllProducts()
            .FirstOrDefault(p => p.Id == productId);

        if (product == null)
            throw new Exception("Product does not exist.");

        int quantity = _input.ReadInt("Quantity:");

        if (quantity <= 0)
            throw new Exception("Quantity must be greater than 0.");

        customer.Cart.AddItem(product, quantity);

        Console.WriteLine("Product added to cart.");
    }

    private bool AskContinueShopping()
    {
        Console.WriteLine("\n1 - Continue shopping");
        Console.WriteLine("2 - Checkout");

        var choice = _input.ReadString("Option:");

        return choice == "1";
    }
}