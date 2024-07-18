using System;
using System.Collections.Generic;
using FIMA.Models;

namespace FIMA
{
    class Program
    {
        static List<Transaction> transactions = new List<Transaction>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Personal Finance Manager");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View Transactions");
                Console.WriteLine("3. Delete Transaction");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddTransaction();
                        break;
                    case "2":
                        ViewTransactions();
                        break;
                    case "3":
                        DeleteTransaction();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        static void AddTransaction()
        {
            Console.Write("Enter description: ");
            string description = Console.ReadLine();
            Console.Write("Enter amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.Write("Enter category: ");
            string category = Console.ReadLine();

            transactions.Add(new Transaction
            {
                Id = transactions.Count + 1,
                Description = description,
                Amount = amount,
                Date = DateTime.Now,
                Category = category
            });

            Console.WriteLine("Transaction added successfully!");
            Console.ReadLine();
        }

        static void ViewTransactions()
        {
            Console.WriteLine("Transactions:");
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.Id}: {transaction.Description} - {transaction.Amount:C} on {transaction.Date.ToShortDateString()} [{transaction.Category}]");
            }
            Console.ReadLine();
        }

        static void DeleteTransaction()
        {
            Console.Write("Enter transaction ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            var transaction = transactions.Find(t => t.Id == id);
            if (transaction != null)
            {
                transactions.Remove(transaction);
                Console.WriteLine("Transaction deleted successfully!");
            }
            else
            {
                Console.WriteLine("Transaction not found.");
            }
            Console.ReadLine();
        }
    }
}
