using System;

class Task_2
{
    static void Main()
    {
        // Core Requirements
        Console.WriteLine("Enter your grade percentage: ");
        int gradePercentage = Convert.ToInt32(Console.ReadLine());

        // Determine letter grade
        string letter;
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Check if the user passed the course
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course with a grade of " + letter);
        }
        else
        {
            Console.WriteLine("Encouragement for next time. Your grade is " + letter);
        }

        // Stretch Challenge
        int lastDigit = gradePercentage % 10;
        string sign = "";

        // Determine the sign
        if (lastDigit >= 7)
        {
            sign = "+";
        }
        else if (lastDigit < 3)
        {
            sign = "-";
        }

        // Handle exceptional cases
        if (letter == "A" && lastDigit >= 7)
        {
            letter = "A-";
            sign = "";
        }
        else if (letter == "F")
        {
            sign = "";
        }

        // Display both the grade letter and the sign in one print statement
        Console.WriteLine("Your final grade: " + letter + sign);
    }
}
