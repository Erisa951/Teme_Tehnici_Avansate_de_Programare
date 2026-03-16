namespace ECommerce.Servicii;

// Serviciu responsabil pentru citirea și validarea inputului de la utilizator
public class InputService
{
    // Citește un număr întreg valid
    public int ReadInt(string message)
    {
        Console.WriteLine(message);

        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
            throw new Exception("Input invalid.");

        if (!int.TryParse(input, out int result))
            throw new Exception("Numar intreg invalid.");

        return result;
    }

    // Citește un număr decimal valid (ex: preț)
    public decimal ReadDecimal(string message)
    {
        Console.WriteLine(message);

        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
            throw new Exception("Input invalid.");

        if (!decimal.TryParse(input, out decimal result))
            throw new Exception("Numar decimal invalid.");

        return result;
    }

    // Citește un text valid
    public string ReadString(string message)
    {
        Console.WriteLine(message);

        var input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
            throw new Exception("Valoare invalida.");

        return input;
    }
}