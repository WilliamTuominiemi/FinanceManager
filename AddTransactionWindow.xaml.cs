using System;
using System.Windows;
using System.Windows.Controls;
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

                var transactionType = ((ComboBoxItem)comboBoxType.SelectedItem)?.Content.ToString();
                if (transactionType == "Expense")
                {
                    amount = -amount;
                }
                Transaction.Amount = amount;

                if (transactionType == "Investment")
                {
                    if (decimal.TryParse(textBoxGrowthRate.Text, out decimal growthRate))
                    {
                        Transaction.GrowthRate = growthRate;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid growth rate.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

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

        private void comboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedType = ((ComboBoxItem)comboBoxType.SelectedItem)?.Content.ToString();
            if (selectedType == "Investment")
            {
                textBlockGrowthRate.Visibility = Visibility.Visible;
                textBoxGrowthRate.Visibility = Visibility.Visible;
            }
            else
            {
                textBlockGrowthRate.Visibility = Visibility.Collapsed;
                textBoxGrowthRate.Visibility = Visibility.Collapsed;
            }
        }
    }
}
