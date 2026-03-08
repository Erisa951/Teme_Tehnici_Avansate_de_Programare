using ECommerce.Models;

namespace ECommerce.Servicii;

// Serviciu care gestionează meniul administratorului
public class AdminMenuService
{
    // Referință către magazin și serviciul de input
    private Store _store;
    private InputService _input;

    // Constructorul primește dependențele necesare
    public AdminMenuService(Store store, InputService input)
    {
        _store = store;
        _input = input;
    }

    // Rulează meniul administratorului
    public void Run()
    {
        while (true)
        {
            try
            {
                // Afișează opțiunile disponibile
                Console.WriteLine("\nADMIN MENU");
                Console.WriteLine("1 - Vezi produse");
                Console.WriteLine("2 - Adauga produs");
                Console.WriteLine("3 - Modifica pret produs");
                Console.WriteLine("4 - Sterge produs");
                Console.WriteLine("0 - Iesire");

                // Citește opțiunea utilizatorului
                var choice = _input.ReadString("Optiune:");

                // Afișează produsele
                if (choice == "1")
                    ShowProducts();

                // Adaugă un produs nou
                else if (choice == "2")
                {
                    int id = _input.ReadInt("ID produs:");
                    string name = _input.ReadString("Nume produs:");
                    decimal price = _input.ReadDecimal("Pret:");

                    _store.Admin.AddProduct(new Product(id, name, price));
                }

                // Modifică prețul unui produs
                else if (choice == "3")
                {
                    int id = _input.ReadInt("ID produs:");
                    decimal price = _input.ReadDecimal("Pret nou:");

                    _store.Admin.UpdateProductPrice(id, price);
                }

                // Șterge un produs
                else if (choice == "4")
                {
                    int id = _input.ReadInt("ID produs:");
                    _store.Admin.RemoveProduct(id);
                }

                // Iese din meniul admin
                else if (choice == "0")
                    break;
            }
            catch (Exception ex)
            {
                // Afișează eventualele erori
                Console.WriteLine(ex.Message);
            }
        }
    }

    // Afișează lista produselor din catalog
    private void ShowProducts()
    {
        foreach (var p in _store.Catalog.GetAllProducts())
            Console.WriteLine($"{p.Id} - {p.Name} - {p.Price} RON");
    }
}