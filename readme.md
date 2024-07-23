# Finance manager
This tool helps you track and manage your personal finances with ease. You can add, edit, and delete transactions, and view a summary of your income, expenses, and net profit/loss.
## Features

1. **Add Transaction**: Easily add new transactions to keep your financial records up to date.
2. **Edit Transaction**: Modify existing transactions to correct any errors or update details.
3. **Delete Transaction**: Remove transactions that are no longer needed.
4. **View Summary**: See a summary of your total income, total expenses, and net profit/loss at a glance.
5. **DataGrid View**: Display all transactions in a structured and easy-to-read format.

## Technologies used

- **C# and .NET**: The application is built using C# and the .NET framework, providing a robust and scalable foundation.
- **WPF (Windows Presentation Foundation)**: Utilized for creating the rich user interface with interactive controls and data binding.
- **XAML**: Used for defining the UI elements in a declarative manner.

## Getting started

### Prerequisites

- .NET SDK: Ensure you have the .NET SDK installed. You can download it from the [official .NET website](https://dotnet.microsoft.com/en-us/download/dotnet-framework) .

1. Clone the repository

```bash
git clone https://github.com/WilliamTuominiemi/FinanceManager.git
cd finance-manager
```

2. Restore dependencies

```bash
dotnet restore
```

3. Build the project

```bash
dotnet build
```

4. Run the application

```bash
dotnet run
```

## Usage
1. Adding a Transaction:
    - Click on the "Add Transaction" button.
    - Fill in the transaction details (Description, Amount, Type, Date).
    - Click "OK" to save the transaction.
2. Editing a Transaction:
    - Select a transaction from the DataGrid.
    - Click on the "Edit Transaction" button.
    - Update the transaction details as needed.
    - Click "OK" to save the changes.
3. Deleting a Transaction:
    - Select a transaction from the DataGrid.
    - Click on the "Delete Transaction" button.
4. Viewing Summary:
    - The total income, total expenses, and net profit/loss are displayed above the DataGrid for quick reference.