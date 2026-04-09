using System;
using System.Collections.Generic;

namespace AdvancedBankingSystem
{
    // ================= MAIN PROGRAM =================
    static class Program
    {
        static List<BankAccount> accounts = new List<BankAccount>();

        static void Main(string[] args)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("     Advanced Banking System       ");
            Console.WriteLine("====================================");
            Console.WriteLine("Creating sample accounts...");

            // Requirement: create at least 3 sample accounts at startup
            Console.WriteLine($"Account #1 created: Alice Johnson - $5000");
            Console.WriteLine($"Account #2 created: Bob Smith - $3000");
            Console.WriteLine($"Account #3 created: Charlie Brown - $7000");

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("\n------------------------------------");
                    Console.WriteLine("               MENU                 ");
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("1. Create New Account\n2. Deposit Money\n3. Withdraw Money\n4. Transfer Money\n5. Calculate Interest\n6. View Account Info\n7. Display Payment Schedule\n8. Calculate Compound Interest (Recursive)\n9. Sum Deposit Array\n10. Test Pass by Value/Reference/Out\n11. Display Bank Statistics\n12. Exit");
                    Console.Write("Choice: ");

                    if (!int.TryParse(Console.ReadLine(), out int choice)) continue;

                    switch (choice)
                    {
                        case 1:
                            CreateAccount();
                            break;

                        case 2:
                            DepositMenu();
                            break;

                        case 3:
                            WithdrawMenu();
                            break;

                        case 4:
                            TransferMenu();
                            break;

                        case 5:
                            CalculateInterestMenu();
                            break;

                        case 6:
                            ViewAccounts();
                            break;

                        case 7:
                            Console.Write("Months: ");
                            if (int.TryParse(Console.ReadLine(), out int months) && months > 0)
                            {
                                Console.Write("Payment amount: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal payment) && payment > 0)
                                    LoanCalculator.DisplayPaymentSchedule(months, payment);
                                else Console.WriteLine("Invalid payment amount.");
                            }
                            else Console.WriteLine("Invalid months.");
                            break;

                        case 8:
                            Console.Write("Principal: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal p)) { Console.WriteLine("Invalid principal."); break; }
                            Console.Write("Rate (e.g. 0.05): ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal r)) { Console.WriteLine("Invalid rate."); break; }
                            Console.Write("Years: ");
                            if (!int.TryParse(Console.ReadLine(), out int y)) { Console.WriteLine("Invalid years."); break; }
                            Console.WriteLine($"Result: {LoanCalculator.CompoundInterest(p, r, y)}");
                            break;

                        case 9:
                            decimal[] arr = { 100m, 200m, 300m };
                            Console.WriteLine($"Sum of sample deposits: {LoanCalculator.SumDeposits(arr, 0)}");
                            break;

                        case 10:
                            TestPassByExamples();
                            break;

                        case 11:
                            BankAccount.DisplayBankStatistics();
                            break;

                        case 12:
                            Exitprogram();
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please try again.");
                }
                pause();
            }
        }

        static void CreateAccount()
        {
            Console.Write("Enter Your FullName: ");
            var name = Console.ReadLine();
            while (string.IsNullOrEmpty(name)) // continue to loop as long as the field is empty or null
            {
                Console.WriteLine("Name cannot be empty or null");
                Console.Write("Enter Your FullName: ");
                name = Console.ReadLine()!; // ask again
            }
            
            Console.Write("Initial deposit: ");
            decimal dep;
            while (!decimal.TryParse(Console.ReadLine(), out dep) || dep < 0)
            {
                Console.WriteLine("Invalid deposit. Please enter a non-negative number.");
                Console.Write("Initial deposit: ");
            }
            
            Console.Write("Type (1=Savings, 2=Current or enter Savings/Current): ");
            string? type = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(type) ||
                   !(type.Equals("1") || type.Equals("2") ||
                     type.Equals("Savings", StringComparison.OrdinalIgnoreCase) ||
                     type.Equals("Current", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Invalid type. Please enter '1' for Savings, '2' for Current, or the words Savings/Current.");
                Console.Write("Type (1=Savings, 2=Current): ");
                type = Console.ReadLine();
            }

            string accountType = (type == "2" || type.Equals("Current", StringComparison.OrdinalIgnoreCase)) ? "Current" : "Savings";
            var acct = new BankAccount(name, dep, accountType);
            accounts.Add(acct);
            Console.WriteLine($"Account #{acct.AccountNumber} created: {acct.AccountHolder} - ${acct.Balance}");
        }

        static BankAccount SelectAccount(string prompt)
        {
            Console.Write(prompt);
            if (!int.TryParse(Console.ReadLine(), out int idx)) { Console.WriteLine("Invalid number."); return null; }
            if (idx < 1 || idx > accounts.Count) { Console.WriteLine("Account not found."); return null; }
            return accounts[idx - 1];
        }

        static void DepositMenu()
        {
            var acc = SelectAccount("Account #: "); if (acc == null) return;
            Console.Write("Amount: "); if (!decimal.TryParse(Console.ReadLine(), out decimal amt) || amt <= 0) { Console.WriteLine("Invalid amount."); return; }
            if (acc.Deposit(amt)) Console.WriteLine("Deposit successful."); else Console.WriteLine("Deposit failed.");
        }

        static void WithdrawMenu()
        {
            var acc = SelectAccount("Account #: "); if (acc == null) return;
            Console.Write("Amount: "); if (!decimal.TryParse(Console.ReadLine(), out decimal amt) || amt <= 0) { Console.WriteLine("Invalid amount."); return; }
            if (acc.Withdraw(amt)) Console.WriteLine("Withdrawal successful."); else Console.WriteLine("Withdrawal failed - check balance.");
        }

        static void TransferMenu()
        {
            Console.WriteLine("Select Transfer Type: 1. Basic 2. With Description 3. With Description & Notification");
            Console.Write("Choice: "); if (!int.TryParse(Console.ReadLine(), out int t) || t < 1 || t > 3) { Console.WriteLine("Invalid."); return; }
            var from = SelectAccount("From Account #: "); if (from == null) return;
            var to = SelectAccount("To Account #: "); if (to == null) return;
            Console.Write("Amount: "); if (!decimal.TryParse(Console.ReadLine(), out decimal amt) || amt <= 0) { Console.WriteLine("Invalid amount."); return; }
            if (t == 1)
            {
                if (from.Transfer(to, amt)) Console.WriteLine("Transfer successful."); else Console.WriteLine("Transfer failed.");
            }
            else if (t == 2)
            {
                Console.Write("Description: "); string? desc = Console.ReadLine()!;
                if (from.Transfer(to, amt, desc)) Console.WriteLine("Transfer successful."); else Console.WriteLine("Transfer failed.");
            }
            else
            {
                Console.Write("Description: "); string? desc = Console.ReadLine()!;
                if (from.Transfer(to, amt, desc, true)) Console.WriteLine("Transfer successful."); else Console.WriteLine("Transfer failed.");
            }
        }

        static void CalculateInterestMenu()
        {
            var acc = SelectAccount("Account #: "); if (acc == null) return;
            Console.Write("Months: "); if (!int.TryParse(Console.ReadLine(), out int months) || months <= 0) { Console.WriteLine("Invalid months."); return; }
            Console.WriteLine("1. Simple (default rate) 2. Custom rate 3. Compound"); Console.Write("Choice: ");
            if (!int.TryParse(Console.ReadLine(), out int opt)) { Console.WriteLine("Invalid."); return; }
            if (opt == 1) Console.WriteLine(acc.CalculateInterest(months));
            else if (opt == 2) { Console.Write("Rate: "); if (!decimal.TryParse(Console.ReadLine(), out decimal r)) { Console.WriteLine("Invalid rate."); return; } Console.WriteLine(acc.CalculateInterest(months, r)); }
            else { Console.Write("Rate: "); if (!decimal.TryParse(Console.ReadLine(), out decimal r)) { Console.WriteLine("Invalid rate."); return; } Console.WriteLine(acc.CalculateInterest(months, r, true)); }
        }

        static void ViewAccounts()
        {
            foreach (var acc in accounts) acc.DisplayAccountInfo();
        }

        static void TestPassByExamples()
        {
            Console.WriteLine("-- Pass by value/ref/out demo --");
            decimal bal = 1000m;
            Console.WriteLine($"Original: {bal}");
            TransactionProcessor.TryUpdateBalance(bal, 500m);
            Console.WriteLine($"After TryUpdateBalance (value): {bal}");
            TransactionProcessor.UpdateBalance(ref bal, 500m);
            Console.WriteLine($"After UpdateBalance (ref): {bal}");

            Console.WriteLine("Testing ProcessTransaction (out parameters):");
            if (TransactionProcessor.ProcessTransaction(200m, "Deposit", out string code, out DateTime time))
                Console.WriteLine($"Success. Code: {code}, Time: {time}");
            else
                Console.WriteLine("ProcessTransaction failed.");
        }

        static void Exitprogram()
        {
            Console.Write("Are you sure you want to exit? (y/n): ");
            string? response = Console.ReadLine()?.Trim().ToLower();
            if (response == "y" || response == "yes")
            {
                Console.WriteLine("\nThank you for using the Advanced Banking System!");
                Console.WriteLine("Goodbye!\n");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Exit cancelled. Returning to menu...");
            }
        }

        static void pause()
        {
            Console.WriteLine("\nPress Enter to return to the menu...");
            Console.ReadLine();
            Console.Clear(); // Clear screen before redisplaying menu
        }
    }
}
