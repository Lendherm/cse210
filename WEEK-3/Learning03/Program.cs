using System;

class Program
{
    static void Main()
    {
        // Create instances using different constructors
        Fraction fraction1 = new Fraction();       // 1/1
        Fraction fraction2 = new Fraction(5);      // 5/1
        Fraction fraction3 = new Fraction(3, 4);   // 3/4
        Fraction fraction4 = new Fraction(1, 3);   // 1/3

        // Display different representations
        DisplayRepresentation(fraction1);
        DisplayRepresentation(fraction2);
        DisplayRepresentation(fraction3);
        DisplayRepresentation(fraction4);
    }

    static void DisplayRepresentation(Fraction fraction)
    {
        Console.WriteLine($"{fraction.GetFractionString()}");

        double decimalValue = fraction.GetDecimalValue();
        Console.WriteLine($"{decimalValue}");
        Console.WriteLine();
    }
}
