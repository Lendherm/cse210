using System;

class Task_3
{
    static void Main()
    {
        string playAgain;
        do
        {
            Console.Write("What is the magic number? ");
            int magicNumber = int.Parse(Console.ReadLine());
            int numberOfGuesses = 0;

            int guess;
            do
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                numberOfGuesses++;

                if (guess < magicNumber)
                    Console.WriteLine("Higher");
                else if (guess > magicNumber)
                    Console.WriteLine("Lower");
                else
                    Console.WriteLine($"You guessed it in {numberOfGuesses} guesses!");

            } while (guess != magicNumber);

            Console.Write("Do you want to play again? (yes/no): ");
            playAgain = Console.ReadLine().ToLower();
        } while (playAgain == "yes");
    }
}
