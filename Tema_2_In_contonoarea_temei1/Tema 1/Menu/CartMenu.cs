using System;
using System.Linq;
using Tema_1.Core;
using Tema_1.Discounts;
using Tema_1.Users.Customer;

namespace Tema_1.Menu;

public class CartMenu
{
    private readonly Store _store;
    private readonly InputService _input;
    private bool _discountApplied = false;

    public CartMenu(Store store, InputService input)
    {
        _store = store;
        _input = input;
    }

    public void ShowCart(Customer customer)
    {
        Console.WriteLine("\nYOUR CART:");

        foreach (var item in customer.Cart.Items)
        {
            decimal total = item.Product.Price * item.Quantity;
            Console.WriteLine($"{item.Product.Name} x{item.Quantity} = {total} RON");
        }

        decimal totalPrice = customer.Cart.GetTotalPrice();
        int itemCount = customer.Cart.Items.Sum(i => i.Quantity);

        Console.WriteLine($"\nProducts total: {totalPrice} RON");

        if (_discountApplied)
        {
            Console.WriteLine("\nA discount is already applied.");
            return;
        }

        ShowDiscountOptions(customer, totalPrice, itemCount);
    }

    private void ShowDiscountOptions(Customer customer, decimal total, int itemCount)
    {
        var discounts = _store.DiscountService
            .GetAvailableDiscounts()
            .ToList();

        if (!discounts.Any())
            return;

        Console.WriteLine("\nAvailable discounts:");
        Console.WriteLine("0 - No discount");

        for (int i = 0; i < discounts.Count; i++)
        {
            var d = discounts[i];

            if (d is PercentageDiscount p)
                Console.WriteLine($"{i + 1} - Code {p.Code} ({p.Percentage}% off)");

            else if (d is MinimumOrderDiscount m)
                Console.WriteLine($"{i + 1} - {m.Percentage}% for orders over {m.Minimum}");

            else if (d is QuantityDiscount q)
                Console.WriteLine($"{i + 1} - {q.Percentage}% for {q.MinItems}+ products");
        }

        int choice = _input.ReadInt("Choose discount:");

        if (choice == 0)
            return;

        if (choice < 1 || choice > discounts.Count)
            throw new Exception("Invalid option.");

        var selected = discounts[choice - 1];

        if (!selected.IsApplicable(total, itemCount))
        {
            Console.WriteLine("Discount conditions not met.");
            return;
        }

        customer.Cart.SetDiscount(selected);
        _discountApplied = true;

        Console.WriteLine("Discount applied.");
    }
}