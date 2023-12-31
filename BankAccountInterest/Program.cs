﻿using BankAccountInterest.Enum;
using BankAccountInterest.Service;
using System;

namespace BankAccountInterest
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to AwesomeGIC Bank! What would you like to do?");
            MainMenu();
        }

        static void MainMenu()
        {
            Console.WriteLine("T - Input transactions \r\nI - Define interest rules\r\nP - Print statement\r\nQ - Quit");
            var userInput = Console.ReadKey();
            var input = userInput.Key.ToString();
            Console.WriteLine();
            if (input.Equals(ExpectingInput.T.ToString()))
            {
                ProcessTransaction();
            }
            else if (input.Equals(ExpectingInput.I.ToString()))
            {
                ProcessDefiningInterestRule();
            }
            else if (input.Equals(ExpectingInput.P.ToString()))
            {
                ProcessPrintingStatement();
            }
            else if (input.Equals(ExpectingInput.Q.ToString()))
            {
                Console.WriteLine("Thank you for banking with AwesomeGIC Bank.\r\nHave a nice day!");
            }
            else
            {
                Console.WriteLine($"Invalid input! : {userInput.KeyChar}");
            }
        }

        static void ProcessTransaction()
        {
            Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format \r\n(or enter blank to go back to main menu):");
            var transactionInput = Console.ReadLine();
            var transactionInputSplit = transactionInput.Split(" ");
            Console.WriteLine();
            if (string.IsNullOrEmpty(transactionInput) || transactionInputSplit.Length != 4)
            {
                Console.WriteLine($"Invalid input! : {transactionInput}");
                MainMenu();
            }
            else
            {
                var transactionService = new TransactionService();
                var response = transactionService.PerformTransaction(transactionInputSplit[0], transactionInputSplit[1], transactionInputSplit[2], transactionInputSplit[3]);
                Console.WriteLine(response);
                Console.WriteLine("Is there anything else you'd like to do?");
                MainMenu();
            }
        }

        static void ProcessDefiningInterestRule()
        {
            Console.WriteLine("Please enter interest rules details in <Date> <RuleId> <Rate in %> format \r\n(or enter blank to go back to main menu):");
            var ruleInput = Console.ReadLine();
            var ruleInputSplit = ruleInput.Split(" ");
            Console.WriteLine();
            if (string.IsNullOrEmpty(ruleInput) || ruleInputSplit.Length != 3)
            {
                Console.WriteLine($"Invalid input! : {ruleInput}");
                MainMenu();
            }
            else
            {
                var ruleDefiningService = new InterestRuleService();
                var response = ruleDefiningService.ProcessInteresRule(ruleInputSplit[0], ruleInputSplit[1], ruleInputSplit[2]);
                Console.WriteLine(response);
                Console.WriteLine("Is there anything else you'd like to do?");
                MainMenu();
            }
        }

        static void ProcessPrintingStatement()
        {
            Console.WriteLine("Please enter account and month to generate the statement <Account> <Year><Month>\r\n(or enter blank to go back to main menu):");
            var printInput = Console.ReadLine();
            var printInputSplit = printInput.Split(" ");
            Console.WriteLine();
            if (string.IsNullOrEmpty(printInput) || printInputSplit.Length != 2)
            {
                Console.WriteLine($"Invalid input! : {printInput}");
                MainMenu();
            }
            else
            {
                var printingService = new PrintingService();
                var response = printingService.ProcessPrintingStatement(printInputSplit[0], printInputSplit[1]);
                Console.WriteLine(response);
                Console.WriteLine("Is there anything else you'd like to do?");
                MainMenu();
            }
        }
    }
}
