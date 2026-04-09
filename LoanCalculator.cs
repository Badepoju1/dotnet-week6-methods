namespace AdvancedBankingSystem
{
    // ================= RECURSION =================
    public class LoanCalculator
    {
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

        public static decimal CompoundInterest(decimal principal, decimal rate, int years)
        {
            if (years == 0) return principal;
            return CompoundInterest(principal * (1 + rate), rate, years - 1);
        }

        public static decimal SumDeposits(decimal[] deposits, int index)
        {
            if (index == deposits.Length) return 0;
            return deposits[index] + SumDeposits(deposits, index + 1);
        }
    }

}

