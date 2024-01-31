using System;

class Program
{
    static void Main()
    {
        Journal journal = new Journal();

        while (true)
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            string choiceInput = Console.ReadLine();

            if (int.TryParse(choiceInput, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        journal.WriteNewEntry();
                        break;
                    case 2:
                        journal.DisplayJournal();
                        break;
                    case 3:
                        journal.SaveJournalToTxtFile();
                        break;
                    case 4:
                        journal.LoadJournalFromTxtFile();
                        break;
                    case 5:
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.\n");
            }
        }
    }
}
