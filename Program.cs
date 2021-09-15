using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;



namespace FirstBankOfSuncoast
{
    class Program
    {

        const string CHECKING_ACCOUNT = "checking.csv";
        const string SAVINGS_ACCOUNT = "savings.csv";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to First Bank of Suncoast");

            // Dummy Data
            List<Transaction> transactions = new List<Transaction>()
            {
              new Transaction("deposit", 100),
              new Transaction("withdrawal", 80)

             };


            bool keepGoing = true;
            string userInput = "";
            string account = "";


            while (keepGoing) {
                Console.Write("Would you like to access your (C)hecking or (S)avings Account? ");
                userInput = Console.ReadLine().ToUpper();

                if (userInput == "C") 
                {
                    account = CHECKING_ACCOUNT;
                } else if (userInput == "S") 
                {
                    account = SAVINGS_ACCOUNT;
                } 
                else
                {
                    Console.WriteLine("Invalid input, bye");
                    return;
                }

                using (var fileReader = new StreamReader(account))
                using (var csvReader = new CsvReader(fileReader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false}))
                {
                     transactions = csvReader.GetRecords<Transaction>().ToList();
                }

                Console.Write("Would you like to (D)eposit, (W)ithdraw, or check (B)alance? ");
                userInput = Console.ReadLine().ToUpper();

                int accountTotal = CalculateTotal(transactions);
                int amountToTransact = 0;


                if (userInput == "D")
                {
                    Console.Write("Enter the amount you would like to deposit:  ");
                    userInput = Console.ReadLine();

                    amountToTransact = int.Parse(userInput);


                    try
                    {
                        amountToTransact = int.Parse(userInput);
                    }
                    catch (Exception e)

                    {
                        Console.WriteLine(e);
                        continue;
                    }

                    if (amountToTransact < 0)
                    {
                        Console.WriteLine("Invalid amount. Please enter a positive number.");
                        continue;
                    }


                    Transaction newTransaction = new Transaction("deposit", amountToTransact);
                    transactions.Add(newTransaction);

                    accountTotal = CalculateTotal(transactions);
                    Console.WriteLine($"Your new total is {accountTotal}");

                }

                else if (userInput == "W")
                {
                    Console.Write("Enter the amount you would like to withdraw:  ");
                    userInput = Console.ReadLine();

                    amountToTransact = int.Parse(userInput);

                    if (amountToTransact < 0)
                    {
                        Console.WriteLine("Invalid amount. Please enter a positive number.");
                        continue;
                    }

                    if (amountToTransact > accountTotal)
                    {
                        Console.WriteLine($"You don't have sufficient funds. Your account balance is {accountTotal}");
                        continue;

                    }

                    Transaction newTransaction = new Transaction("withdrawal", amountToTransact);
                    transactions.Add(newTransaction);

                    accountTotal = CalculateTotal(transactions);
                    Console.WriteLine($"Your new total is {accountTotal}");
                }
                else if (userInput == "B")
                {
                    Console.WriteLine($"Your account total is: {accountTotal}");

                }
                else
                {
                    Console.WriteLine("Invalid input, bye");
                    return;
                }


              using (var fileWriter = new StreamWriter(account))
              using (var csvWriter = new CsvWriter(fileWriter, new CsvConfiguration(CultureInfo.InvariantCulture){ HasHeaderRecord = false}))
              {
                  csvWriter.WriteRecords(transactions);
              }

                Console.Write("Would you like to make another transaction? Y/N ");
                userInput = Console.ReadLine().ToUpper();
                if (userInput == "N")
                {
                    keepGoing = false;
                    Console.Write("Thank you, see you next time.");
                }
            }

        }

        public static int CalculateTotal(List<Transaction> transactions)
        {
            int sum = 0;

            foreach (Transaction item in transactions)
            {
                if (item.Action == "deposit")
                {
                    sum += item.Amount;
                }
                else if (item.Action == "withdrawal")
                {
                    sum -= item.Amount;
                }
            }
            return sum;

        }
        public static void ShowTransactionHistory(List<Transaction> transactions)
        {
            transactions.ForEach(x => Console.WriteLine($"{x.Action}, {x.Amount}"));
        }
    }

    public class Transaction
    {
        public string Action { get; set; }
        public int Amount { get; set; }
        public Transaction(string action, int amount)
        {
            Action = action;
            Amount = amount;
        }


    }
}
