using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using FinanceManager.Models;
using System.Windows.Controls;

namespace FinanceManager
{
    public partial class MainWindow : Window
    {
        private List<Transaction> Transactions { get; set; }
        private string filePath = "transactions.csv";

        public MainWindow()
        {
            InitializeComponent();
            Transactions = new List<Transaction>();
            LoadTransactions();
            dataGridTransactions.ItemsSource = Transactions;
        }

        private void ButtonAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            var newTransactionId = Transactions.Count + 1;
            var addTransactionWindow = new AddTransactionWindow(newTransactionId);
            if (addTransactionWindow.ShowDialog() == true)
            {
                Transactions.Add(addTransactionWindow.Transaction);
                SaveTransactions();
                dataGridTransactions.Items.Refresh();
            }
        }

        private void ButtonDeleteTransaction_Click(object sender, RoutedEventArgs e)
        {
            var selectedTransaction = (Transaction)dataGridTransactions.SelectedItem;
            if (selectedTransaction != null)
            {
                Transactions.Remove(selectedTransaction);
                SaveTransactions();
                dataGridTransactions.Items.Refresh();
            }
        }

        private void SaveTransactions()
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Id,Date,Description,Amount,GrowthRate");
                foreach (var transaction in Transactions)
                {
                    writer.WriteLine($"{transaction.Id},{transaction.Date},{transaction.Description},{transaction.Amount},{transaction.GrowthRate?.ToString() ?? string.Empty}");
                }
            }
        }

        private void LoadTransactions()
        {
            if (!File.Exists(filePath))
                return;

            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine(); // Skip header
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');

                    if (values.Length < 5)
                    {
                        MessageBox.Show("Invalid data format in transactions file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        continue; // Skip malformed lines
                    }

                    try
                    {
                        var transaction = new Transaction
                        {
                            Id = int.Parse(values[0]),
                            Date = DateTime.Parse(values[1]),
                            Description = values[2],
                            Amount = decimal.Parse(values[3]),
                            GrowthRate = string.IsNullOrWhiteSpace(values[4]) ? (decimal?)null : decimal.Parse(values[4])
                        };
                        Transactions.Add(transaction);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error parsing transaction data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        continue; // Skip the problematic line and continue
                    }
                }
            }
        }
    }
}
