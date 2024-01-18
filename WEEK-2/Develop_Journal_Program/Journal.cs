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
        Console.Write("Location: ");
        string location = Console.ReadLine();
        Console.Write("Weather: ");
        string weather = Console.ReadLine();
        Console.Write("Mood: ");
        string mood = Console.ReadLine();
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        entries.Add(new JournalEntry(randomPrompt, response, date, location, weather, mood));
        Console.WriteLine("Entry added successfully!\n");
    }

    // Method to display all entries in the journal
    public void DisplayJournal()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine($"Location: {entry.Location}");
            Console.WriteLine($"Weather: {entry.Weather}");
            Console.WriteLine($"Mood: {entry.Mood}\n");
        }
    }

    // Method to save the journal to a file
    public void SaveJournalToTxtFile()
    {
        Console.Write("Enter filename to save journal as TXT: ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine($"Date: {entry.Date}");
                writer.WriteLine($"Prompt: {entry.Prompt}");
                writer.WriteLine($"Response: {entry.Response}");
                writer.WriteLine($"Location: {entry.Location}");
                writer.WriteLine($"Weather: {entry.Weather}");
                writer.WriteLine($"Mood: {entry.Mood}\n");
            }
        }

        Console.WriteLine("Journal saved to TXT file successfully!\n");
    }
    // Method to load the journal from a file
    public void LoadJournalFromTxtFile()
    {
        Console.Write("Enter filename to load journal from TXT: ");
        string filename = Console.ReadLine();

        entries.Clear(); // Clear existing entries

        using (StreamReader reader = new StreamReader(filename))
        {
            while (!reader.EndOfStream)
            {
                string date = reader.ReadLine().Replace("Date: ", "");
                string prompt = reader.ReadLine().Replace("Prompt: ", "");
                string response = reader.ReadLine().Replace("Response: ", "");
                string location = reader.ReadLine().Replace("Location: ", "");
                string weather = reader.ReadLine().Replace("Weather: ", "");
                string mood = reader.ReadLine().Replace("Mood: ", "");

                entries.Add(new JournalEntry(prompt, response, date, location, weather, mood));

                // Skip the newline between entries
                if (!reader.EndOfStream)
                    reader.ReadLine();
            }
        }

        Console.WriteLine("Journal loaded from TXT file successfully!\n");
    }
}
