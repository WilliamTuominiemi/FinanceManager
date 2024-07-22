using System;
using System.Windows;
using System.Windows.Controls; // Add this namespace
using FinanceManager.Models;

namespace FinanceManager
{
    public partial class AddTransactionWindow : Window
    {
        public Transaction Transaction { get; private set; }

        public AddTransactionWindow(int id)
        {
            InitializeComponent();
            Transaction = new Transaction { Id = id, Date = DateTime.Now };
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(textBoxAmount.Text, out decimal amount))
            {
                Transaction.Description = textBoxDescription.Text;
                Transaction.Category = ((ComboBoxItem)comboBoxCategory.SelectedItem)?.Content.ToString();

                var transactionType = ((ComboBoxItem)comboBoxType.SelectedItem)?.Content.ToString();
                if (transactionType == "Expense")
                {
                    amount = -amount;
                }
                Transaction.Amount = amount;

                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid amount.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
