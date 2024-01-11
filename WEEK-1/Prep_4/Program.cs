using System;
using System.Collections.Generic;
using System.Linq;

class Task_4
{
    static void Main()
    {
        // Create a list to store the numbers
        List<int> numbers = new List<int>();

        // Get numbers from the user until 0 is entered
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        while (true)
        {
            Console.Write("Enter number: ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 0)
                break;

            numbers.Add(input);
        }

        // Core Requirements

        // Compute the sum of the numbers
        int sum = numbers.Sum();

        // Compute the average of the numbers
        double average = numbers.Average();

        // Find the maximum number in the list
        int max = numbers.Max();

        // Display core results
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");

        // Stretch Challenge

        // Find the smallest positive number (closest to zero)
        int smallestPositive = numbers.Where(x => x > 0).DefaultIfEmpty(0).Min();

        // Sort the numbers and display the sorted list
        numbers.Sort();
        Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}

