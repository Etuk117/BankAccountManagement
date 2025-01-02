using System;

namespace BankAccountManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "accountData.json"; // File to save and load account data
            Account account1;

            // Load existing account data or create a new account
            Console.WriteLine("Attempting to load account data...");
            account1 = Account.LoadAccountData(filePath) ?? new Account(123456789, "Aniekan Etuk", 100000);

            Console.WriteLine("Welcome to the Bank Account Management System!");

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Check Balance");
                Console.WriteLine("4. Display Account Details");
                Console.WriteLine("5. View Transaction History");
                Console.WriteLine("6. Save Account Data");
                Console.WriteLine("7. Load Account Data");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");

                string? input = Console.ReadLine();
                int choice;

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1: // Deposit
                        Console.Write("Enter deposit amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                        {
                            account1.Deposit(depositAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount. Please enter a valid number.");
                        }
                        break;

                    case 2: // Withdraw
                        Console.Write("Enter withdrawal amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                        {
                            account1.Withdraw(withdrawAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount. Please enter a valid number.");
                        }
                        break;

                    case 3: // Check Balance
                        account1.DisplayBalance();
                        break;

                    case 4: // Display Account Details
                        account1.DisplayAccountDetails();
                        break;

                    case 5: // View Transaction History
                        account1.DisplayTransactions();
                        break;

                    case 6: // Save Account Data
                        account1.SaveAccountData(filePath);
                        break;

                    case 7: // Load Account Data
                        account1 = Account.LoadAccountData(filePath) ?? account1;
                        break;

                    case 8: // Exit
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
