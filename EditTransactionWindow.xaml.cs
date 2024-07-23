using System;
using System.Windows;
using System.Windows.Controls;
using FinanceManager.Models;

namespace FinanceManager
{
    public partial class EditTransactionWindow : Window
    {
        public Transaction Transaction { get; private set; }

        public EditTransactionWindow(Transaction transaction)
        {
            InitializeComponent();
            Transaction = transaction;

            // Populate the fields with the transaction details
            textBoxDescription.Text = Transaction.Description;
            textBoxAmount.Text = Transaction.Amount.ToString();
            comboBoxType.SelectedItem = comboBoxType.Items
                .Cast<ComboBoxItem>()
                .FirstOrDefault(item => item.Content.ToString() == Transaction.Type);
            
            if (Transaction.Type == "Investment")
            {
                textBlockGrowthRate.Visibility = Visibility.Visible;
                textBoxGrowthRate.Visibility = Visibility.Visible;
                textBoxGrowthRate.Text = Transaction.GrowthRate?.ToString() ?? string.Empty;
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            // Update the transaction details
            Transaction.Description = textBoxDescription.Text;
            if (decimal.TryParse(textBoxAmount.Text, out decimal amount))
            {
                Transaction.Amount = amount;
            }

            var selectedItem = comboBoxType.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                Transaction.Type = selectedItem.Content.ToString();
            }

            if (Transaction.Type == "Investment")
            {
                if (decimal.TryParse(textBoxGrowthRate.Text, out decimal growthRate))
                {
                    Transaction.GrowthRate = growthRate;
                }
            }
            else
            {
                Transaction.GrowthRate = null; // Clear growth rate if type is not investment
            }

            DialogResult = true;
            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void comboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = comboBoxType.SelectedItem as ComboBoxItem;
            if (selectedItem != null && selectedItem.Content.ToString() == "Investment")
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
