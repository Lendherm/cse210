using System;

class Program
{
    static void Main()
    {
        // Create a Writing assignment
        WritingAssignment writingAssignment = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");

        // Get and display the summary
        Console.WriteLine(writingAssignment.GetSummary());

        // Get and display the writing information
        string writingInfo = writingAssignment.GetWritingInformation();
        Console.WriteLine(writingInfo);
    }
}
