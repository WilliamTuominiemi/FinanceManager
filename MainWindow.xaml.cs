using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using FinanceManager.Models;
using System.Windows.Controls;

namespace FinanceManager
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
        private string filePath = "transactions.csv";

        public MainWindow()
        {
            InitializeComponent();
            Transactions = new ObservableCollection<Transaction>();
            LoadTransactions();
            UpdateTotals();
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
                UpdateTotals();
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
                UpdateTotals();
                dataGridTransactions.Items.Refresh();
            }
        }

        private void ButtonEditTransaction_Click(object sender, RoutedEventArgs e)
        {
            var selectedTransaction = (Transaction)dataGridTransactions.SelectedItem;
            if (selectedTransaction != null)
            {
                var editTransactionWindow = new EditTransactionWindow(selectedTransaction);
                if (editTransactionWindow.ShowDialog() == true) 
                {
                    SaveTransactions();
                    dataGridTransactions.Items.Refresh();
                }
            }
        }


        private void SaveTransactions()
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Id,Date,Description,Amount,Type,GrowthRate");
                foreach (var transaction in Transactions)
                {
                    writer.WriteLine($"{transaction.Id},{transaction.Date},{transaction.Description},{transaction.Amount},{transaction.Type},{transaction.GrowthRate?.ToString() ?? string.Empty}");
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

                    if (values.Length < 6)
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
                            Type = values[4],
                            GrowthRate = string.IsNullOrWhiteSpace(values[5]) ? (decimal?)null : decimal.Parse(values[5])
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

        private void UpdateTotals()
        {
            decimal totalIncome = Transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
            decimal totalExpense = Transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);
            decimal totalInvested = Transactions.Where(t => t.Type == "Investment").Sum(t => t.Amount);
            decimal netProfitLoss = totalIncome + totalExpense;
            decimal liquidCash = netProfitLoss - totalInvested;


            textBlockTotalIncome.Text = totalIncome.ToString("C");
            textBlockTotalExpense.Text = totalExpense.ToString("C");
            textBlockTotalInvested.Text = totalInvested.ToString("C");
            textBlockNetProfitLoss.Text = netProfitLoss.ToString("C");
            textBlockNetLiquid.Text = liquidCash.ToString("C");
        }
    }
}
