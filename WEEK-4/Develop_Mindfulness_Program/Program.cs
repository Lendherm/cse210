using System;
using System.Threading;

// Base class for activities
public abstract class Activity
{
    private string name;
    private string description;
    protected int duration;

    public Activity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    // Common starting message for all activities

    private static Dictionary<string, int> activityCount = new Dictionary<string, int>();


    public void StartActivity()
    {
        // Increment the counter for this activity type
        if (activityCount.ContainsKey(name))
        {
            activityCount[name]++;
        }
        else
        {
            activityCount[name] = 1;
        }

        Console.WriteLine($"Starting {name} activity (#{activityCount[name]} times)...");

        Console.WriteLine(description);
        SetDuration();
        Console.WriteLine($"Get ready to begin. Starting in 3 seconds...");
        Thread.Sleep(3000);
    }
    public static void PrintActivityCounts()
    {
        Console.WriteLine("\nActivity Counts:");
        foreach (var kvp in activityCount)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value} times");
        }
        Console.WriteLine();
    }

    // Common ending message for all activities
    public void EndActivity()
    {
        Console.WriteLine($"Good job! You have completed the {name} activity for {duration} seconds.");
        Thread.Sleep(3000);
    }

    // Prompt user for activity duration
    private void SetDuration()
    {
        Console.Write("Enter the duration in seconds: ");
        duration = Convert.ToInt32(Console.ReadLine());
    }

    // Abstract method to be implemented in derived classes
    public abstract void DoActivity();

}

// Breathing activity class
public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public override void DoActivity()
    {
        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine(i % 2 == 0 ? "Breathe in..." : "Breathe out...");
            Thread.Sleep(1000); // Adjust for your preferred duration
        }
    }
    
}

// Reflection activity class
public class ReflectionActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        // ... Add more prompts as needed
    };

    private string[] reflectionQuestions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you feel when it was complete?",
        "What challenges did you face during this experience?",
        "In what ways did this experience change you?",
        "Did you learn something new about yourself?",
        "What strengths did you discover in yourself?",
        "Would you approach a similar situation differently now?",
        "How can you apply the lessons from this experience to your current life?",
        // ... Add more reflection questions as needed
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    public override void DoActivity()
    {
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);

        int remainingDuration = duration;

        while (remainingDuration > 0)
        {
            string question = reflectionQuestions[random.Next(reflectionQuestions.Length)];
            Console.WriteLine(question);
            Thread.Sleep(10000); // Display a random question every 10 seconds
            remainingDuration -= 10;
        }

        // Pause for the remaining duration if any
        if (remainingDuration > 0)
        {
            Console.WriteLine($"Remaining time: {remainingDuration} seconds.");
            Thread.Sleep(remainingDuration * 1000);
        }
    }
}

// Listing activity class
public class ListingActivity : Activity
{
    private string[] listingPrompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        // ... Add more listing prompts as needed
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    public override void DoActivity()
    {
        Random random = new Random();
        string prompt = listingPrompts[random.Next(listingPrompts.Length)];
        Console.WriteLine(prompt);

        Thread.Sleep(3000); // Pause for 3 seconds before starting the listing

        Console.WriteLine("Get ready to list. Starting in 3 seconds...");
        Thread.Sleep(3000); // Pause for 3 seconds before starting the listing

        Console.WriteLine("Go!");

        int remainingDuration = duration;
        int numberOfItems = 0;

        // Allow the user to list items for the specified duration
        Console.WriteLine("Type your items and press Enter:");

        // Create a separate thread for user input to avoid blocking the countdown display
        Thread inputThread = new Thread(() =>
        {
            while (remainingDuration > 0)
            {
                string? input = Console.ReadLine(); // Add nullability check here
                if (input != null && input.Trim() != "")
                {
                    Console.WriteLine($"Item: {input}");
                    numberOfItems += input.Split(',').Length; // Adjust for the number of items entered
                }
            }
        });

        inputThread.Start();

        // Display a reminder every 10 seconds during the listing
        while (remainingDuration > 0)
        {
            Console.WriteLine($"Remaining time: {remainingDuration} seconds.");
            Thread.Sleep(10000); // Display a reminder every 10 seconds
            remainingDuration -= 10;
        }

        // Wait for the user to finish inputting items
        inputThread.Join();

        Console.WriteLine($"You listed {numberOfItems} items.");

        // Pause for the remaining duration if any
        if (remainingDuration > 0)
        {
            Console.WriteLine($"Remaining time: {remainingDuration} seconds.");
            Thread.Sleep(remainingDuration * 1000);
        }
    }
}

// Running activity class
public class RunningActivity : Activity
{
    public RunningActivity() : base("Running", "This activity will get your heart pumping by engaging in a running exercise. Lace up your shoes and get ready to hit the pavement!")
    {
    }

    public override void DoActivity()
    {
        Console.WriteLine("Start running!");

        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine($"Running... {i + 1} seconds");
            Thread.Sleep(1000); // Adjust for your preferred duration
        }
    }
}

//Keeping a log of how many times activities were performed.



// Main program
class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing");
            Console.WriteLine("2. Reflection");
            Console.WriteLine("3. Listing");
            Console.WriteLine("4. Running");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            Activity activity;

            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity();
                    break;
                case 2:
                    activity = new ReflectionActivity();
                    break;
                case 3:
                    activity = new ListingActivity();
                    break;
                case 4:
                    activity = new RunningActivity();
                    break;
                case 5:
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }

            activity.StartActivity();
            activity.DoActivity();
            activity.EndActivity();
            Activity.PrintActivityCounts();
        }
    }
}
