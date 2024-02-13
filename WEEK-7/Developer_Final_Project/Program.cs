using System;
using System.Collections.Generic;

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

// Class to manage accounts
public class Account
{
    private decimal balance;
    private List<Transaction> transactions;

    public Account()
    {
        balance = 0;
        transactions = new List<Transaction>();
    }

    public decimal Balance => balance;

    public void Deposit(decimal amount)
    {
        balance += amount;
        transactions.Add(new Income(DateTime.Now, "Deposit", amount));
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
    }

    public void PrintTransactions()
    {
        Console.WriteLine("Transaction History:");
        foreach (var transaction in transactions)
        {
            Console.WriteLine($"{transaction.Date.ToShortDateString()} - {transaction.Description}: ${transaction.Amount}");
        }
    }
}

// Class to manage budgets
public class BudgetManager
{
    private Dictionary<string, decimal> categories;

    public BudgetManager()
    {
        categories = new Dictionary<string, decimal>();
    }

    public void AddCategory(string category, decimal amount)
    {
        categories.Add(category, amount);
    }

    public void RemoveCategory(string category)
    {
        categories.Remove(category);
    }

    public void UpdateBudget(string category, decimal amount)
    {
        if (categories.ContainsKey(category))
        {
            categories[category] = amount;
        }
        else
        {
            Console.WriteLine("Category not found.");
        }
    }

    public void PrintBudget()
    {
        Console.WriteLine("Budget Categories:");
        foreach (var category in categories)
        {
            Console.WriteLine($"{category.Key}: ${category.Value}");
        }
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        Account myAccount = new Account();
        BudgetManager myBudget = new BudgetManager();

        myAccount.Deposit(2000);
        myAccount.Withdraw(1000);

        myBudget.AddCategory("Food", 300);
        myBudget.AddCategory("Entertainment", 200);

        myAccount.PrintTransactions();
        myBudget.PrintBudget();
    }
}
