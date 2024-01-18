using System;
using System.Collections.Generic;

public class Journal
{
    private List<JournalEntry> entries = new List<JournalEntry>();
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    // Method to write a new entry
    public void WriteNewEntry()
    {
        string randomPrompt = prompts[new Random().Next(prompts.Count)];
        Console.WriteLine($"Prompt: {randomPrompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        entries.Add(new JournalEntry(randomPrompt, response, date));
        Console.WriteLine("Entry added successfully!\n");
    }

    // Method to display all entries in the journal
    public void DisplayJournal()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}\n");
        }
    }

    // Method to save the journal to a file
    public void SaveJournalToFile()
    {
        Console.Write("Enter filename to save journal: ");
        string filename = Console.ReadLine();
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
            }
        }
        Console.WriteLine("Journal saved to file successfully!\n");
    }

    // Method to load the journal from a file
    public void LoadJournalFromFile()
    {
        Console.Write("Enter filename to load journal: ");
        string filename = Console.ReadLine();
        entries.Clear();
        using (StreamReader reader = new StreamReader(filename))
        {
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(',');
                entries.Add(new JournalEntry(line[1], line[2], line[0]));
            }
        }
        Console.WriteLine("Journal loaded from file successfully!\n");
    }
}
