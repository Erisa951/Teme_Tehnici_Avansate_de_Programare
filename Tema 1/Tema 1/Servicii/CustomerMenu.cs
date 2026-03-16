using ECommerce.Models;
using ECommerce.Strategii;
using ECommerce.Interfete;

namespace ECommerce.Servicii;

// Serviciu care gestionează meniul și interacțiunea clientului cu magazinul
public class CustomerMenuService
{
    // Referințe către componentele principale ale aplicației
    private Store _store;
    private OrderService _orderService;
    private InputService _input;

    // Constructorul primește dependențele necesare
    public CustomerMenuService(Store store, OrderService orderService, InputService input)
    {
        _store = store;
        _orderService = orderService;
        _input = input;
    }

    // Metoda principală care rulează meniul clientului
    public void Run()
    {
        // Crearea coșului de cumpărături fără discount implicit
        var cart = new ShoppingCart(new NoDiscount());

        // Crearea clientului și asocierea coșului
        var customer = new Customer(2, "Client", cart);

        // Bucla principală a meniului
        while (true)
        {
            try
            {
                // Afișează produsele disponibile
                ShowProducts();

                Console.WriteLine("\nIntrodu ID produs:");
                Console.WriteLine("0 - Vezi cosul");

                // Citirea opțiunii utilizatorului
                int id = _input.ReadInt("Optiune:");

                // Dacă utilizatorul vrea să vadă coșul
                if (id == 0)
                {
                    ShowCart(customer);

                    Console.WriteLine("\n1 - Continua cumparaturile");
                    Console.WriteLine("2 - Alege firma de curierat");

                    var choice = _input.ReadString("Optiune:");

                    // Continuă cumpărăturile
                    if (choice == "1")
                        continue;

                    // Finalizează comanda
                    if (choice == "2")
                    {
                        ChooseShippingAndPayment(customer);
                        break;
                    }

                    throw new Exception("Optiune invalida.");
                }

                // Caută produsul în catalog
                var product = _store.Catalog.FindById(id);

                if (product == null)
                    throw new Exception("Produs inexistent.");

                // Citește cantitatea dorită
                int qty = _input.ReadInt("Cantitate:");

                if (qty <= 0)
                    throw new Exception("Cantitatea trebuie sa fie > 0.");

                // Adaugă produsul în coș
                customer.Cart.Add(product, qty);

                Console.WriteLine("Produs adaugat in cos.");
            }
            catch (Exception ex)
            {
                // Tratarea erorilor de input sau logică
                Console.WriteLine($"Eroare: {ex.Message}");
            }
        }
    }

    // Metodă care afișează conținutul coșului și verifică posibile discounturi
    private void ShowCart(Customer customer)
    {
        try
        {
            Console.WriteLine("\nCOSUL TAU:");

            // Parcurge produsele din coș
            foreach (var item in customer.Cart.GetItems())
            {
                Console.WriteLine($"{item.Product.Name} x{item.Quantity} = {item.Product.Price * item.Quantity} RON");
            }

            // Calcul total produse
            decimal total = customer.Cart.GetTotal();

            Console.WriteLine($"\nTotal produse: {total} RON");

            // Verifică discountul pentru comandă minimă
            var minDiscount = _store.DiscountService
                .GetAvailableDiscounts()
                .OfType<MinimumOrderDiscount>()
                .FirstOrDefault();

            if (minDiscount != null && total < minDiscount.Minimum)
            {
                decimal diff = minDiscount.Minimum - total;

                Console.WriteLine($"Mai adauga produse de {diff} RON pentru discount de {minDiscount.Percentage}%.");
            }

            // Verifică discountul pentru număr minim de produse
            var qtyDiscount = _store.DiscountService
                .GetAvailableDiscounts()
                .OfType<QuantityDiscount>()
                .FirstOrDefault();

            int totalItems = customer.Cart.GetTotalQuantity();

            if (qtyDiscount != null && totalItems < qtyDiscount.MinItems)
            {
                int diff = qtyDiscount.MinItems - totalItems;

                Console.WriteLine($"Mai adauga {diff} produse pentru discount de {qtyDiscount.Percentage}%.");
            }

            // Introducere cod de reducere
            Console.WriteLine("\nIntrodu cod de reducere (ENTER daca nu ai):");

            string? code = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(code))
            {
                // Caută codul în lista de discounturi
                var found = _store.DiscountService
                    .GetAvailableDiscounts()
                    .OfType<PercentageDiscount>()
                    .FirstOrDefault(d => d.Code == code);

                if (found != null)
                {
                    // Aplică discountul în coș
                    customer.Cart.SetDiscount(found);
                    Console.WriteLine("Cod valid. Discount aplicat.");
                }
                else
                {
                    throw new Exception("Cod de reducere invalid.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare cos: {ex.Message}");
        }
    }

    // Metodă pentru alegerea livrării și a metodei de plată
    private void ChooseShippingAndPayment(Customer customer)
    {
        try
        {
            // Obține metodele de livrare disponibile
            var shippingMethods = _store.ShippingService.GetAvailableMethods();

            // Gruparea metodelor după firma de curierat
            var couriers = shippingMethods
                .GroupBy(s => s.GetCourierName().Split(" - ")[0])
                .ToList();

            Console.WriteLine("\nAlege firma de curierat:");

            for (int i = 0; i < couriers.Count; i++)
                Console.WriteLine($"{i} - {couriers[i].Key}");

            int courierChoice = _input.ReadInt("Optiune:");

            if (courierChoice < 0 || courierChoice >= couriers.Count)
                throw new Exception("Curier inexistent.");

            var selectedCourier = couriers[courierChoice].ToList();

            Console.WriteLine("\nAlege metoda de livrare:");

            for (int i = 0; i < selectedCourier.Count; i++)
            {
                decimal cost = selectedCourier[i].CalculateShippingCost(customer.Cart.GetTotal());

                var methodName = selectedCourier[i].GetCourierName().Split(" - ")[1];

                Console.WriteLine($"{i} - {methodName} - {cost} RON");
            }

            int methodChoice = _input.ReadInt("Optiune:");

            if (methodChoice < 0 || methodChoice >= selectedCourier.Count)
                throw new Exception("Metoda livrare invalida.");

            var shipping = selectedCourier[methodChoice];

            decimal productsTotal = customer.Cart.GetTotal();
            decimal shippingCost = shipping.CalculateShippingCost(productsTotal);

            Console.WriteLine($"\nCost transport: {shippingCost} RON");

            // Alegerea metodei de plată
            Console.WriteLine("\nMetode de plata:");
            Console.WriteLine("1 - Card");
            Console.WriteLine("2 - PayPal");
            Console.WriteLine("3 - ApplePay");

            var paymentChoice = _input.ReadString("Optiune:");

            // Strategy Pattern pentru metoda de plată
            IPaymentStrategy payment =
                paymentChoice == "2" ? new PayPalPayment() :
                paymentChoice == "3" ? new ApplePayPayment() :
                new CardPayment();

            // Crearea comenzii
            var order = _orderService.CreateOrder(customer.Cart, payment, shipping);

            Console.WriteLine($"\nComanda creata: {order.Id}");
            Console.WriteLine($"Total produse: {productsTotal} RON");
            Console.WriteLine($"Transport: {shippingCost} RON");
            Console.WriteLine($"Total final: {productsTotal + shippingCost} RON");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare livrare/plata: {ex.Message}");
        }
    }

    // Metodă care afișează produsele din catalog
    private void ShowProducts()
    {
        var products = _store.Catalog.GetAllProducts();

        if (products == null)
            throw new Exception("Catalog produse indisponibil.");

        Console.WriteLine("\nProduse disponibile:");

        foreach (var p in products)
        {
            Console.WriteLine($"{p.Id} - {p.Name} - {p.Price} RON");
        }
    }
}