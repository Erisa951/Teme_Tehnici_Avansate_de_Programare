using System.Linq;
using Tema_1.Core;
using Tema_1.Catalog;
using Tema_1.Users.Admin;

namespace Tema_1.Menu;

public class AdminMenuService
{
    private readonly Store _store;
    private readonly InputService _input;
    private readonly AdminOperationsService _adminOperations;

    public AdminMenuService(Store store, AdminOperationsService adminOperations, InputService input)
    {
        _store = store;
        _adminOperations = adminOperations;
        _input = input;
    }

    public void Run()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("\nADMIN MENU");
                Console.WriteLine("1 - View products");
                Console.WriteLine("2 - Add product");
                Console.WriteLine("3 - Update product price");
                Console.WriteLine("4 - Remove product");
                Console.WriteLine("0 - Exit");

                var choice = _input.ReadString("Option:");

                switch (choice)
                {
                    case "1":
                        ShowProducts();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        UpdateProductPrice();
                        break;
                    case "4":
                        RemoveProduct();
                        break;
                    case "0":
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private void ShowProducts()
    {
        _store.Catalog.GetAllProducts()
            .Select(p => $"{p.Id} - {p.Name} - {p.Price} RON")
            .ToList()
            .ForEach(Console.WriteLine);
    }

    private void AddProduct()
    {
        int id = _input.ReadInt("Product ID:");
        string name = _input.ReadString("Product name:");
        decimal price = _input.ReadDecimal("Price:");
        string category = _input.ReadString("Category:");

        _adminOperations.AddProduct(new Product(id, name, category, price));
    }

    private void UpdateProductPrice()
    {
        int id = _input.ReadInt("Product ID:");
        decimal price = _input.ReadDecimal("New price:");

        _adminOperations.UpdateProductPrice(id, price);
    }

    private void RemoveProduct()
    {
        int id = _input.ReadInt("Product ID:");
        _adminOperations.RemoveProduct(id);
    }
}