using System.Linq;

namespace Tema_1.Core;

public class InputService
{
    public int ReadInt(string message)
    {
        Console.Write(message);

        var input = Console.ReadLine();

        var value =
            (from c in (input ?? "")
             where char.IsDigit(c) || c == '-'
             select c).ToArray();

        if (!value.Any())
            throw new Exception("Invalid integer value.");

        if (!int.TryParse(new string(value), out var result))
            throw new Exception("Invalid integer value.");

        return result;
    }

    public decimal ReadDecimal(string message)
    {
        Console.Write(message);

        var input = Console.ReadLine();

        var value =
            (from c in (input ?? "")
             where char.IsDigit(c) || c == '.' || c == '-'
             select c).ToArray();

        if (!value.Any())
            throw new Exception("Invalid decimal value.");

        if (!decimal.TryParse(new string(value), out var result))
            throw new Exception("Invalid decimal value.");

        return result;
    }

    public string ReadString(string message)
    {
        Console.Write(message);

        var input = Console.ReadLine() ?? "";

        var value = input
            .Where(c => !char.IsControl(c))
            .ToArray();

        if (!value.Any())
            throw new Exception("Invalid value.");

        return new string(value);
    }
}