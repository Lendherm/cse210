using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Scripture Hiding Program!");

        // Lista de escrituras con escrituras predeterminadas
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture("Romans 15:13", "May the God of hope fill you with all joy and peace as you trust in him, so that you may overflow with hope by the power of the Holy Spirit."),
            new Scripture("Ephesians 3:20", "Now to him who is able to do immeasurably more than all we ask or imagine, according to his power that is at work within us."),
            new Scripture("Romans 8:38-39", "For I am convinced that neither death nor life, neither angels nor demons, neither the present nor the future, nor any powers, neither height nor depth, nor anything else in all creation, will be able to separate us from the love of God that is in Christ Jesus our Lord."),
            new Scripture("Lamentations 3:22-23", "The steadfast love of the Lord never ceases; his mercies never come to an end; they are new every morning; great is your faithfulness."),
            new Scripture("2 Corinthians 4:16-18", "So we do not lose heart. Though our outer self is wasting away, our inner self is being renewed day by day. For this light momentary affliction is preparing for us an eternal weight of glory beyond all comparison, as we look not to the things that are seen but to the things that are unseen."),
            new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),
            new Scripture("Proverbs 3:5-6", "Trust in the LORD with all your heart and lean not on your own understanding; in all your ways acknowledge him, and he will make your paths straight.")
        };

        // Menú principal
        while (true)
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Add Scripture");
            Console.WriteLine("2. View Scriptures");
            Console.WriteLine("3. Memorize Scriptures");
            Console.WriteLine("4. Delete Scripture");
            Console.WriteLine("5. Quit");
            Console.Write("Select an option (1-5): ");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    AddScripture(scriptures);
                    break;
                case "2":
                    ViewScriptures(scriptures);
                    break;
                case "3":
                    MemorizeScriptures(scriptures);
                    break;
                case "4":
                    DeleteScripture(scriptures);
                    break;
                case "5":
                    Console.WriteLine("Program ended. Press any key to exit.");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("Invalid option. Please enter a number from 1 to 5.");
                    break;
            }
        }
    }

    static void AddScripture(List<Scripture> scriptures)
    {
        Console.WriteLine("Adding a new scripture:");

        // Requerir una referencia con al menos 5 letras
        string reference;
        do
        {
            Console.WriteLine("Enter the reference (at least 5 letters):");
            reference = Console.ReadLine();
        } while (reference.Length < 5);

        // Requerir un texto con al menos 2 palabras
        string text;
        do
        {
            Console.WriteLine("Enter the text (at least 2 words):");
            text = Console.ReadLine();
        } while (text.Split(' ').Length < 2);

        Scripture newScripture = new Scripture(reference, text);
        scriptures.Add(newScripture);

        Console.WriteLine("New scripture added successfully!");
        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }




    static void ViewScriptures(List<Scripture> scriptures)
    {
        Console.WriteLine("\nViewing Scriptures:");

        if (scriptures.Any())
        {
            foreach (var scripture in scriptures)
            {
                Console.WriteLine($"{scripture.Reference}: {scripture.GetHiddenText()}");
            }
        }
        else
        {
            Console.WriteLine("No scriptures available.");
        }

        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }


    static void MemorizeScriptures(List<Scripture> scriptures)
    {
        if (!scriptures.Any())
        {
            Console.WriteLine("No scriptures available to memorize.");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            return;
        }

        // Seleccionar aleatoriamente una escritura para empezar
        Scripture currentScripture = GetRandomScripture(scriptures);

        // Mostrar la escritura completa inicialmente
        DisplayScripture(currentScripture);

        // Continuar ocultando palabras hasta que todas las palabras estén ocultas o el usuario elija salir
        while (!currentScripture.AllWordsHidden())
        {
            Console.WriteLine("Press Enter to hide more words, type 'quit' to exit, or answer the hidden words.");
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "quit")
                break;

            // Ocultar algunas palabras al azar
            HideRandomWords(currentScripture);

            // Mostrar la escritura modificada
            DisplayScripture(currentScripture);

            // Permitir al usuario responder a las palabras ocultas
            AnswerHiddenWords(currentScripture);
        }

        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }

    static void DeleteScripture(List<Scripture> scriptures)
    {
        Console.WriteLine("Deleting a scripture:");
        if (!scriptures.Any())
        {
            Console.WriteLine("No scriptures available to delete.");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Choose a scripture to delete:");

        for (int i = 0; i < scriptures.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {scriptures[i].Reference}");
        }

        Console.Write("Enter the number of the scripture to delete: ");
        if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= scriptures.Count)
        {
            Scripture deletedScripture = scriptures[selectedIndex - 1];
            scriptures.RemoveAt(selectedIndex - 1);

            Console.WriteLine($"{deletedScripture.Reference} deleted successfully!");
        }
        else
        {
            Console.WriteLine("Invalid selection. No scripture deleted.");
        }

        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }

    static void ShowAvailableScriptures(List<Scripture> scriptures)
    {
        Console.WriteLine("Available Scriptures:");
        foreach (var scripture in scriptures)
        {
            Console.WriteLine($"{scripture.Reference}: {scripture.GetHiddenText()}");
        }
    }

    static void DisplayScripture(Scripture scripture)
    {
        Console.Clear();
        Console.WriteLine($"Scripture Reference: {scripture.Reference}");
        Console.WriteLine($"Scripture Text: {scripture.GetHiddenText()}");
    }

    static void HideRandomWords(Scripture scripture)
    {
        Random random = new Random();
        int wordsToHide = random.Next(1, 4); // Ocultar de 1 a 3 palabras

        for (int i = 0; i < wordsToHide; i++)
        {
            // Seleccionar aleatoriamente una palabra y ocultarla
            int wordIndex = random.Next(scripture.WordCount);
            scripture.HideWord(wordIndex);
        }
    }

    static void AnswerHiddenWords(Scripture scripture)
    {
        for (int i = 0; i < scripture.WordCount; i++)
        {
            if (scripture.IsWordHidden(i))
            {
                Console.Write($"Enter the answer for hidden word {i + 1}: ");
                string userAnswer = Console.ReadLine();

                // Verificar si la respuesta del usuario coincide con la palabra original
                if (userAnswer.ToLower() != scripture.GetWordAtIndex(i).ToLower())
                {
                    Console.WriteLine($"Incorrect answer. The correct word is: {scripture.GetWordAtIndex(i)}");
                    scripture.UnhideWord(i);
                }
                else
                {
                    Console.WriteLine("Correct answer!");
                }
            }
        }
    }

    static Scripture GetRandomScripture(List<Scripture> scriptures)
    {
        Random random = new Random();
        return scriptures[random.Next(scriptures.Count)];
    }
}

class Scripture
{
    private string reference;
    private string text;
    private bool[] hiddenWords;

    public string Reference { get { return reference; } }
    public int WordCount { get { return text.Split(' ').Length; } }

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        this.text = text;
        this.hiddenWords = new bool[WordCount];
    }

    public string GetHiddenText()
    {
        string[] words = text.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (hiddenWords[i])
                words[i] = new string('*', words[i].Length);
        }

        return string.Join(" ", words);
    }

    public void HideWord(int index)
    {
        if (index >= 0 && index < WordCount)
            hiddenWords[index] = true;
    }

    public bool IsWordHidden(int index)
    {
        return hiddenWords[index];
    }

    public string GetWordAtIndex(int index)
    {
        string[] words = text.Split(' ');
        return words[index];
    }

    public void UnhideWord(int index)
    {
        if (index >= 0 && index < WordCount)
            hiddenWords[index] = false;
    }

    public bool AllWordsHidden()
    {
        return hiddenWords.All(word => word == true);
    }

}