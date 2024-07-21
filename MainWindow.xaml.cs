// MainWindow.xaml.cs
using System.Collections.ObjectModel;
using System.Windows;
using FinanceManager.Models;

namespace FinanceManager
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Transaction> transactions;
        private int nextId = 1;

        public MainWindow()
        {
            InitializeComponent();  
            transactions = new ObservableCollection<Transaction>();
            dataGridTransactions.ItemsSource = transactions;
        }

        private void ButtonAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            var addTransactionWindow = new AddTransactionWindow(nextId);
            if (addTransactionWindow.ShowDialog() == true)
            {
                transactions.Add(addTransactionWindow.Transaction);
                nextId++;
            }
        }

        private void ButtonDeleteTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTransactions.SelectedItem is Transaction selectedTransaction)
            {
                transactions.Remove(selectedTransaction);
            }
            else
            {
                MessageBox.Show("Please select a transaction to delete.", "Delete Transaction", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
