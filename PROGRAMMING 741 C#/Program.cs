using System;

namespace MotorVehicleExpenses
{
    //expenses
    abstract class Expense
    {
        public double Amount { get; protected set; }

        public abstract void Calculate();
    }

    // vehicle loan
    class CarLoan : Expense
    {
        private double purchasePrice;
        private double totalDeposit;
        private double interestRate;
        private int numberOfMonths;

        public CarLoan(double purchasePrice, double totalDeposit, double interestRate, int numberOfMonths)
        {
            this.purchasePrice = purchasePrice;
            this.totalDeposit = totalDeposit;
            this.interestRate = interestRate;
            this.numberOfMonths = numberOfMonths;
        }

        public override void Calculate()
        {
            double loanAmount = purchasePrice - totalDeposit;
            double monthlyInterestRate = interestRate / 100 / 12;
            double monthlyPayment = (loanAmount * monthlyInterestRate) / (1 - Math.Pow(1 + monthlyInterestRate, -numberOfMonths));
            Amount = monthlyPayment;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
           
            Console.Write("Enter your monthly gross income: ");
            double grossIncome = Convert.ToDouble(Console.ReadLine());

         
            Console.Write("Enter your monthly tax deductions: ");
            double taxDeductions = Convert.ToDouble(Console.ReadLine());

        
            double netIncome = grossIncome - taxDeductions;


            double[] vehicleExpenses = new double[4];
            Console.WriteLine("Enter your motor vehicle expenses:");
            Console.Write("Fuel cost: ");
            vehicleExpenses[0] = Convert.ToDouble(Console.ReadLine());
            Console.Write("Insurance cost: ");
            vehicleExpenses[1] = Convert.ToDouble(Console.ReadLine());
            Console.Write("Parking fees: ");
            vehicleExpenses[2] = Convert.ToDouble(Console.ReadLine());
            Console.Write("Maintenance costs: ");
            vehicleExpenses[3] = Convert.ToDouble(Console.ReadLine());

 
            double totalExpenses = 0;
            foreach (var expense in vehicleExpenses)
            {
                totalExpenses += expense;
            }

          
            Console.WriteLine($"Your net income: {netIncome:C}");
            Console.WriteLine($"Total expenses: {totalExpenses:C}");

           
            Console.WriteLine("Choose between buying or hiring a Motor Vehicle (Enter 'buy' or 'hire'): ");
            string choice = Console.ReadLine();

            if (choice.ToLower() == "hire")
            {
              
                Console.Write("Enter hiring cost per month: ");
                double hiringCost = Convert.ToDouble(Console.ReadLine());

                double amountLeftAfterExpenses = netIncome - totalExpenses - hiringCost;

                Console.WriteLine($"Your gross income: {grossIncome:C}");
                Console.WriteLine($"Your net income: {netIncome:C}");
                Console.WriteLine($"Amount left after all expenses are paid: {amountLeftAfterExpenses:C}");
            }
            else if (choice.ToLower() == "buy")
            {
             
                Console.Write("Enter purchase price of the motor vehicle: ");
                double purchasePrice = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter total deposit: ");
                double totalDeposit = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter interest rate in percentage: ");
                double interestRate = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter number of months to repay (between 120 and 240 months): ");
                int numberOfMonths = Convert.ToInt32(Console.ReadLine());

                if (numberOfMonths < 120 || numberOfMonths > 240)
                {
                    Console.WriteLine("Number of months must be between 120 and 240.");
                    return;
                }


                CarLoan vehicleLoan = new CarLoan(purchasePrice, totalDeposit, interestRate, numberOfMonths);
                vehicleLoan.Calculate();

                double amountLeftAfterExpenses = netIncome - totalExpenses - vehicleLoan.Amount;

                Console.WriteLine($"Your gross income: {grossIncome:C}");
                Console.WriteLine($"Your net income: {netIncome:C}");
                Console.WriteLine($"Amount left after all expenses are paid: {amountLeftAfterExpenses:C}");

 
                if (vehicleLoan.Amount > grossIncome / 3)
                {
                    Console.WriteLine("Approval for the loan is unlikely due to high monthly repayment.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }
}
