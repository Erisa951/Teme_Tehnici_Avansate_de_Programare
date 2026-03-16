namespace ECommerce.Models;

// Clasă abstractă care reprezintă un utilizator general al aplicației
public abstract class User
{
    // Identificatorul utilizatorului
    public int Id { get; }

    // Numele utilizatorului
    public string Name { get; }

    // Constructor protejat care inițializează datele utilizatorului
    protected User(int id, string name)
    {
        Id = id;
        Name = name;
    }

    // Metodă abstractă care va fi implementată de clasele derivate
    // pentru a specifica rolul utilizatorului (Admin sau Customer)
    public abstract string GetRole();
}