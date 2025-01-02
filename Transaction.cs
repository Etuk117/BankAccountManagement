using System;

namespace BankAccountManagement
{
    internal class Transaction
    {
        public string Type { get; set; } // Type of transaction (e.g., "Deposit" or "Withdrawal")
        public decimal Amount { get; set; } // Amount involved in the transaction
        public DateTime Date { get; set; } // Date of the transaction

        // Constructor for initializing a new transaction
        public Transaction(string type, decimal amount, DateTime? date = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type), "Transaction type cannot be null");
            Amount = amount;
            Date = date ?? DateTime.Now; // Default to the current date if not provided
        }

        // Override ToString for better readability when displaying transactions
        public override string ToString()
        {
            return $"{Date.ToShortDateString()} - {Type}: {Amount:C}";
        }
    }
}
