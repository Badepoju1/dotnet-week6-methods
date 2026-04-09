namespace AdvancedBankingSystem
{
    // ================= RECURSION =================
    /// Utility methods demonstrating recursion: payment schedules, compound interest
    /// and summing arrays recursively. Methods are static since this is a helper class.
    
    public static class LoanCalculator
    {
        /// Display a simple payment schedule recursively by counting down months.
        public static void DisplayPaymentSchedule(int months, decimal payment)
        {
            if (months == 0)
            {
                Console.WriteLine("Loan fully paid!");
                return;
            }

            Console.WriteLine($"Month {months}: Pay {payment}");
            DisplayPaymentSchedule(months - 1, payment);
        }

        /// Compute compound interest recursively. Each recursive call multiplies
        /// the principal by (1 + rate) for the remaining years.
        public static decimal CompoundInterest(decimal principal, decimal rate, int years)
        {
            if (years == 0) return principal;
            return CompoundInterest(principal * (1 + rate), rate, years - 1);
        }

        /// Sum an array of deposits recursively starting at the provided index.
        public static decimal SumDeposits(decimal[] deposits, int index)
        {
            if (index == deposits.Length) return 0;
            return deposits[index] + SumDeposits(deposits, index + 1);
        }
    }

}

