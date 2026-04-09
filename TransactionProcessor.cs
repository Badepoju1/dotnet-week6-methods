using System;

namespace AdvancedBankingSystem
{
    // ================= TRANSACTION PROCESSOR =================
    /// Helper functions demonstrating different parameter passing modes (value, ref, out)
    /// and providing utility methods for creating accounts and logging transactions.
    public static class TransactionProcessor
    {

        // Pass by value - caller's balance is NOT modified //
        /// Demonstrates pass-by-value: modifies a local copy of the balance and prints it.
        /// The caller's variable is not changed.
        public static void TryUpdateBalance(decimal balance, decimal amount)
        {
            balance += amount;
            Console.WriteLine($"[TryUpdateBalance] local balance = {balance}");
        }

        // Pass by ref - caller's balance IS modified
        /// Demonstrates pass-by-ref: the caller's balance variable is updated.
        public static void UpdateBalance(ref decimal balance, decimal amount)
        {
            balance += amount;
            Console.WriteLine($"[UpdateBalance] ref balance = {balance}");
        }

        // Pass by out - returns success and outputs confirmation values
        /// Simulate processing a transaction. Returns true on success and outputs
        /// a short confirmation code and timestamp via out parameters.
        public static bool ProcessTransaction(decimal amount, string type, out string confirmationCode, out DateTime timestamp)
        {
            if (amount <= 0)
            {
                confirmationCode = string.Empty;
                timestamp = default;
                return false;
            }

            confirmationCode = Guid.NewGuid().ToString("N").Substring(0, 8);
            timestamp = DateTime.Now;
            return true;
        }
        
        // -------- OPTIONAL PARAMETER METHOD (REQUIRED TASK) ---------
        /// Create a Savings account with optional parameters for interest, minimum balance and branch.
        /// Writes a brief message and returns a new BankAccount instance.
        public static BankAccount CreateSavingsAccount(
            string accountHolder,
            decimal initialDeposit,
            decimal interestRate = 0.02m,
            int minimumBalance = 1000,
            string branch = "Main Branch")
        {
            Console.WriteLine($"Account created for {accountHolder} at {branch}");
            Console.WriteLine($"Interest Rate: {interestRate}, Min Balance: {minimumBalance}");

            return new BankAccount(accountHolder, initialDeposit, "Savings");
        }

        /// Log a transaction to the console with optional description, email flag and category.
        public static void LogTransaction(string type, decimal amount, string description = "No description", bool sendEmail = false, string category = "General")
        {
            Console.WriteLine($"[{category}] {type}: {amount} - {description}");
            if (sendEmail) Console.WriteLine("Email sent");
        }
    }
}
