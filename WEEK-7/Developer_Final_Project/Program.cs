using System;
using System.Collections.Generic;
using System.IO;

// Base class for financial transactions
public class Transaction
{
    public DateTime Date { get; }
    public string Description { get; }
    public decimal Amount { get; }

    public Transaction(DateTime date, string description, decimal amount)
    {
        Date = date;
        Description = description;
        Amount = amount;
    }
}

// Class to represent income transactions
public class Income : Transaction
{
    public Income(DateTime date, string description, decimal amount) : base(date, description, amount)
    {
    }
}

// Class to represent expense transactions
public class Expense : Transaction
{
    public Expense(DateTime date, string description, decimal amount) : base(date, description, amount)
    {
    }
}

public class BudgetCategory
{
    public string Name { get; }
    public decimal Amount { get; }

    public BudgetCategory(string name, decimal amount)
    {
        Name = name;
        Amount = amount;
    }
}

public class BudgetManager
{
    public const string BudgetFileName = "budgetdata.txt";

    private List<BudgetCategory> categories;

    public BudgetManager()
    {
        categories = LoadBudget();
        if (categories.Count == 0)
        {
            // Add some initial budget categories if the file is empty or doesn't exist
            AddCategory("Groceries", 200);
            AddCategory("Entertainment", 50);
        }
    }

    public List<BudgetCategory> GetAllCategories()
    {
        return categories;
    }

    public void SaveAllCategories()
    {
        SaveBudget();
        Console.WriteLine("All budget categories saved.");
    }

    private void SaveBudget()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(BudgetFileName))
            {
                foreach (var category in categories)
                {
                    if (category != null)
                    {
                        writer.WriteLine($"{category.Name},{category.Amount}");
                    }
                }
            }
            Console.WriteLine("Budget data saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving budget data: {ex.Message}");
        }
    }


    private List<BudgetCategory> LoadBudget()
    {
        List<BudgetCategory> loadedCategories = new List<BudgetCategory>();

        // Load the categories from a file
        try
        {
            if (File.Exists(BudgetFileName))
            {
                string[] lines = File.ReadAllLines(BudgetFileName);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && decimal.TryParse(parts[1], out decimal amount))
                    {
                        loadedCategories.Add(new BudgetCategory(parts[0], amount));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading budget data: {ex.Message}");
        }

        return loadedCategories;
    }

    public void AddCategory(string name, decimal amount)
{
    var newCategory = new BudgetCategory(name, amount);
    categories.Add(newCategory);
    Console.WriteLine($"Budget category '{name}' added with budgeted amount: ${amount}");
    PrintBudget();
    SaveBudget(); // Asegúrate de guardar el presupuesto después de agregar una categoría
    Console.WriteLine("Budget data saved successfully."); // Mensaje adicional
}


    public void RemoveCategory(string name)
    {
        var categoryToRemove = categories.Find(c => c?.Name == name);
        if (categoryToRemove != null)
        {
            categories.Remove(categoryToRemove);
            Console.WriteLine($"Budget category '{name}' removed.");
            PrintBudget();
        }
        else
        {
            Console.WriteLine("Category not found.");
        }
    }

    public void UpdateBudget(string name, decimal amount)
    {
        var categoryToUpdate = categories.Find(c => c.Name == name);
        if (categoryToUpdate != null)
        {
            // Create a new BudgetCategory instance with the updated amount
            var updatedCategory = new BudgetCategory(categoryToUpdate.Name, amount);

            // Replace the existing category with the updated one
            int index = categories.IndexOf(categoryToUpdate);
            categories[index] = updatedCategory;

            Console.WriteLine($"Budget category '{name}' updated with new budgeted amount: ${amount}");
            PrintBudget();
        }
        else
        {
            Console.WriteLine("Category not found.");
        }
    }

    public void PrintBudget()
    {
        Console.WriteLine("Updated Budget Categories:");
        foreach (var category in categories)
        {
            if (category != null)
            {
                Console.WriteLine($"{category.Name}: ${category.Amount}");
            }
        }
        Console.WriteLine();
    }
}

// Class to manage accounts
// Class to manage accounts
public class Account
{
    private decimal balance;
    private List<Transaction> transactions;

    public Account()
    {
        LoadBalance();  // Load the balance during initialization
        transactions = new List<Transaction>();
    }

    public decimal Balance => balance;

    public void Deposit(decimal amount)
    {
        balance += amount;
        transactions.Add(new Income(DateTime.Now, "Deposit", amount));
        SaveBalance();  // Save the updated balance
    }

    public void Withdraw(decimal amount)
    {
        if (amount > balance)
        {
            Console.WriteLine("Insufficient funds.");
            return;
        }

        balance -= amount;
        transactions.Add(new Expense(DateTime.Now, "Withdrawal", amount));
        SaveBalance();  // Save the updated balance
    }

    public void PrintTransactions()
    {
        Console.WriteLine("Transaction History:");
        foreach (var transaction in transactions)
        {
            Console.WriteLine($"{transaction.Date.ToShortDateString()} - {transaction.Description}: ${transaction.Amount}");
        }
    }

    private void SaveBalance()
    {
        // Save the balance to a file or another persistent storage
        // For demonstration purposes, let's print the balance to the console
        Console.WriteLine($"Balance saved: ${balance}");
    }

    private void LoadBalance()
    {
        // Load the balance from a file or another persistent storage
        // For demonstration purposes, you can set an initial balance
        // Alternatively, you can implement logic to load the actual saved balance
        balance = 1000;  // Set an initial balance (you can replace this with your logic)
        Console.WriteLine($"Initial Balance loaded: ${balance}");
    }
}


// Main program
class Program
{
    static void Main(string[] args)
    {
        Account myAccount = new Account();
        BudgetManager myBudget = new BudgetManager();

        // Verificar si el archivo budgetdata.txt existe al inicio
        if (File.Exists(BudgetManager.BudgetFileName))
        {
            Console.WriteLine("Found budgetdata.txt.");
        }
        else
        {
            Console.WriteLine("budgetdata.txt not found. Creating a new one.");
        }


        Console.WriteLine($"Initial Balance: ${myAccount.Balance}\n");

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Add Budget Category");
            Console.WriteLine("4. Remove Budget Category");
            Console.WriteLine("5. Update Budget");
            Console.WriteLine("6. View Transactions");
            Console.WriteLine("7. View Budget");
            Console.WriteLine("8. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter deposit amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                        {
                            myAccount.Deposit(depositAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount.");
                        }
                        break;
                    case 2:
                        Console.Write("Enter withdrawal amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                        {
                            myAccount.Withdraw(withdrawAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount.");
                        }
                        break;
                    case 3:
                        Console.Write("Enter category name: ");
                        string categoryName = Console.ReadLine();
                        Console.Write("Enter budgeted amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal budgetAmount))
                        {
                            myBudget.AddCategory(categoryName, budgetAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount.");
                        }
                        break;
                    case 4:
                        Console.Write("Enter category name to remove: ");
                        string categoryToRemove = Console.ReadLine();
                        myBudget.RemoveCategory(categoryToRemove);
                        break;
                    case 5:
                        Console.Write("Enter category name to update: ");
                        string categoryToUpdate = Console.ReadLine();
                        Console.Write("Enter new budgeted amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newBudgetAmount))
                        {
                            myBudget.UpdateBudget(categoryToUpdate, newBudgetAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount.");
                        }
                        break;
                    case 6:
                        myAccount.PrintTransactions();
                        break;
                    case 7:
                        myBudget.PrintBudget();
                        break;
                    case 8:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter a number.");
            }

            Console.WriteLine($"Current Balance: ${myAccount.Balance}\n");
        }
    }
}
