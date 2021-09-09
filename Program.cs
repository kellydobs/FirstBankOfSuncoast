using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;


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
              new Transaction("withdrawl", 80)

             };

            // Creates a stream reader to get information from our file


            // var fileReader = new StreamReader(CHECKING_ACCOUNT);


            // if (File.Exists(CHECKING_ACCOUNT))
            // {
            //     fileReader = new StreamReader(CHECKING_ACCOUNT);
            // }
            // else


            // var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture);
            // List<Transaction> checkingTranscations = csvReader.GetRecords<Transaction>().ToList();

            // foreach (Transaction item in checkingTransactions)
            // {
            //     Console.WriteLine($"{item.Action} , {item.Amount} ");

            // }



            bool keepGoing = true;
            string userInput = "";
            string account = "";


            while (keepGoing)
            {
                Console.Write("Would you like to access your (C)hecking or (S)avings Account? ");
                userInput = Console.ReadLine().ToUpper();

                if (userInput == "C")
                {
                    account = CHECKING_ACCOUNT;
                }
                else if (userInput == "S")
                {
                    account = SAVINGS_ACCOUNT;
                }
                else
                {
                    Console.WriteLine("Invalid input, bye");
                    return;
                }

                Console.Write("Would you like to (D)eposit, (W)ithdraw, or check (B)alance? ");
                userInput = Console.ReadLine().ToUpper();

                int accountTotal = CalculateTotal(transcations);
                int amountToTransact = 0;


                if (userInput == "D")
                {
                    Console.Write("Enter the amount you would like to deposit:  ");
                    userInput = Console.ReadLine();

                    amountToTransact = int.Parse(userInput);

                    if (amountToTransact < 0)
                    {
                        Console.WriteLine("Invalid amount. Please enter a positive number.");
                        continue;
                    }


                    Transaction newTransaction = new Transaction("deposit", amountToTransact);
                    transactions.Add(newTransaction);

                    accountTotal = CalculateTotal(transcations);
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

                    Transaction newTransaction = new Transaction("withdraw", amountToTransact);
                    transactions.Add(newTransaction);

                    accountTotal = CalculateTotal(transcations);
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


                // // Create a stream for writing information into a file
                // var fileWriter = new StreamWriter(CHECKING_ACCOUNT);

                // // Create an object that can write CSV to the fileWriter
                // var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
                // // write transactions to checking.csv 
                // // Ask our csvWriter to write out our list of numbers
                // csvWriter.WriteRecords(checkingTransactions);
                // // Tell the file we are done
                // fileWriter.Close();


                Console.Write("Would you like to make another transaction? Y/N ");
                userInput = Console.ReadLine().ToUpper();
                if (userInput == "N")
                {
                    keepGoing = false;
                }
            }


        }
        public static int CalculateTotal(List<Transaction> transcations)
        {
            int sum = 0;

            foreach (Transaction item in transactions)
            {
                if (item.Action == "deposit")
                {
                    sum += item.Amount;
                }
                else if (item.Action == "withdraw")
                {
                    sum -= item.Amount;
                }
            }
            return sum;

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
