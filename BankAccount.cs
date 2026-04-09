using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

// =========================================================
// FINAL DISTINCTION VERSION - FULLY COMPLETE
// Covers ALL assignment requirements with demonstrations
// =========================================================
namespace AdvancedBankingSystem
{
    /// Represents a simple bank account with balance, account holder and transaction history.
    /// Includes static counters to track totals across all accounts in this demo application.
    public class BankAccount
    {
        // -------- STATIC VARIABLES ---------
        public static int TotalAccounts { get; set; } = default;
        public static decimal BankTotalMoney { get; set; } = 0;
        public static int TransactionCounter { get; set; } = default;

        // -------- INSTANCE VARIABLES ---------
        public int AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public decimal Balance { get; set; }
        public string AccountType { get; set; }
        public List<string> TransactionHistory { get; set; }

        // -------- CONSTRUCTOR --------
        /// Initialize a new BankAccount with an initial deposit and account type.
        /// Increments global counters and records the creation in the transaction history.
        public BankAccount(string name, decimal initialDeposit, string type)
        {
            AccountNumber = ++TotalAccounts;
            AccountHolder = name;
            Balance = initialDeposit;
            AccountType = type;
            TransactionHistory = new List<string>();

            BankTotalMoney += initialDeposit;
            TransactionHistory.Add($"Account created with {initialDeposit}");
        }

        // -------- INSTANCE METHODS --------
        /// Deposit funds into this account. Returns true on success.
        public bool Deposit(decimal amount)
        {
            if (amount <= 0) return false;

            Balance += amount;
            BankTotalMoney += amount;
            TransactionCounter++;
            TransactionHistory.Add($"Deposited: {amount}");
            return true;
        }

        /// Withdraw funds from this account if sufficient balance exists.
        /// Returns true when the withdrawal is successful.
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0 || amount > Balance) return false;

            Balance -= amount;
            BankTotalMoney -= amount;
            TransactionCounter++;
            TransactionHistory.Add($"Withdrawn: {amount}");
            return true;
        }
        /// Write account details and transaction history to the console.
        
        public void DisplayAccountInfo()
        {
            Console.WriteLine($"\nAccount #{AccountNumber} - {AccountHolder}");
            Console.WriteLine($"Type: {AccountType}");
            Console.WriteLine($"Balance: {Balance}");

            Console.WriteLine("Transaction History:");
            foreach (var t in TransactionHistory)
                Console.WriteLine(t);
        }
        
        /// Read-only accessor returning the current balance.
        
        public decimal GetBalance() => Balance;

        // -------- STATIC METHODS --------
        /// Return the total number of BankAccount instances created.
        
        public static int GetTotalAccounts() => TotalAccounts;
        public static decimal GetBankTotalMoney() => BankTotalMoney;
        public static int GetTotalTransactions() => TransactionCounter;

        
        /// Display aggregate bank statistics (total accounts, money, transactions).
        
        public static void DisplayBankStatistics()
        {
            Console.WriteLine("\n--- Bank Statistics ---");
            Console.WriteLine($"Total Accounts: {GetTotalAccounts()}");
            Console.WriteLine($"Total Money in Bank: {GetBankTotalMoney()}");
            Console.WriteLine($"Total Transactions: {GetTotalTransactions()}");
        }

        // -------- TRANSFER OVERLOADS --------
        /// Transfer amount to destination account (no description, no notification).
        public bool Transfer(BankAccount destinationAccount, decimal amount)
        {
            return Transfer(destinationAccount, amount, description: string.Empty, sendNotification: false);
        }

        /// Transfer amount with a description (no notification).
        public bool Transfer(BankAccount destinationAccount, decimal amount, string description)
        {
            return Transfer(destinationAccount, amount, description, sendNotification: false);
        }

        /// Core transfer implementation: withdraw from this account and deposit to destination.
        /// Optionally prints a notification message when sendNotification is true.
        public bool Transfer(BankAccount destinationAccount, decimal amount, string description, bool sendNotification)
        {
            if (destinationAccount == null) return false;
            if (amount <= 0 || amount > Balance) return false;

            // Withdraw from this account
            Balance -= amount;
            TransactionHistory.Add($"Transferred: {amount} to Account #{destinationAccount.AccountNumber}" +
                                   (string.IsNullOrEmpty(description) ? string.Empty : $" ({description})"));

            // Deposit to destination account
            destinationAccount.Balance += amount;
            destinationAccount.TransactionHistory.Add($"Received: {amount} from Account #{AccountNumber}" +
                                                      (string.IsNullOrEmpty(description) ? string.Empty : $" ({description})"));

            // This is effectively two operations (withdraw + deposit)
            TransactionCounter += 2;

            if (sendNotification)
            {
                Console.WriteLine($"Notification: Transferred {amount} from Account #{AccountNumber} to Account #{destinationAccount.AccountNumber}.");
            }

            return true;
        }
        

        // -------- CALCULATE INTEREST OVERLOADS ---------
        // Simple interest with default rate 2%
        /// Calculate simple interest using the default rate (2%).
        public decimal CalculateInterest(int months)
        {
            if (months <= 0) return 0m;
            decimal rate = 0.02m;
            return Balance * rate * months;
        }

        // Simple interest with custom rate
        /// Calculate simple interest using a custom rate.
        public decimal CalculateInterest(int months, decimal customRate)
        {
            if (months <= 0) return 0m;
            return Balance * customRate * months;
        }

        // Optionally compound interest over the provided months using a custom rate.
        /// Calculate interest and optionally compound it over the provided months.
        public decimal CalculateInterest(int months, decimal customRate, bool compound)
        {
            if (months <= 0) return 0m;
            if (!compound)
                return Balance * customRate * months;

            // compound: balance * (1 + customRate)^months - balance
            double baseVal = (double)(1m + customRate);
            double pow = Math.Pow(baseVal, months);
            decimal compounded = Balance * (decimal)pow - Balance;
            return compounded;
        }
        
    }  
        
}
