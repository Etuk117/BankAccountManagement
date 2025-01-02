using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BankAccountManagement
{
    internal class Account
    {
        public int AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }

        // Constructor to initialize the account
        public Account(int accountNumber, string accountHolder, decimal balance)
        {
            AccountNumber = accountNumber;
            AccountHolder = accountHolder;
            Balance = balance;
            Transactions = new List<Transaction>();
        }

        // Method to deposit money
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Transactions.Add(new Transaction("Deposit", amount)); // Now, no need to explicitly pass Date, it'll default
                Console.WriteLine($"Deposited {amount:C}. New Balance: {Balance:C}");
            }
            else
            {
                Console.WriteLine("Deposit amount must be positive.");
            }
        }

        // Method to withdraw money
        public void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                Transactions.Add(new Transaction("Withdraw", amount)); // Now, no need to explicitly pass Date, it'll default
                Console.WriteLine($"Withdrew {amount:C}. New Balance: {Balance:C}");
            }
            else if (amount > Balance)
            {
                Console.WriteLine("Insufficient balance for this withdrawal.");
            }
            else
            {
                Console.WriteLine("Withdrawal amount must be positive.");
            }
        }

        // Method to display the balance
        public void DisplayBalance()
        {
            Console.WriteLine($"Account Balance: {Balance:C}");
        }

        // Method to display account details
        public void DisplayAccountDetails()
        {
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Account Holder: {AccountHolder}");
            Console.WriteLine($"Balance: {Balance:C}");
        }

        // Method to display all transactions
        public void DisplayTransactions()
        {
            Console.WriteLine("Transaction History:");
            foreach (var transaction in Transactions)
            {
                Console.WriteLine($"{transaction.Date.ToShortDateString()} - {transaction.Type}: {transaction.Amount:C}");
            }
        }

        // Method to save account data to a file
        public void SaveAccountData(string filePath)
        {
            try
            {
                var accountData = new
                {
                    AccountNumber,
                    AccountHolder,
                    Balance,
                    Transactions
                };

                string json = JsonSerializer.Serialize(accountData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
                Console.WriteLine("Account data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving account data: {ex.Message}");
            }
        }

        // Method to load account data from a file
        public static Account LoadAccountData(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var accountData = JsonSerializer.Deserialize<dynamic>(json);

                    Account account = new Account(
                        (int)accountData.AccountNumber,
                        (string)accountData.AccountHolder,
                        (decimal)accountData.Balance
                    );

                    foreach (var transaction in accountData.Transactions)
                    {
                        account.Transactions.Add(new Transaction(
                            (string)transaction.Type,
                            (decimal)transaction.Amount,
                            DateTime.Parse((string)transaction.Date)
                        ));
                    }

                    Console.WriteLine("Account data loaded successfully.");
                    return account;
                }
                else
                {
                    Console.WriteLine("No account data found. Starting fresh.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading account data: {ex.Message}");
                return null;
            }
        }
    }
}
