using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BankCalculator.Models;

namespace BankCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Please enter the name of the file, path to the out data folder and ISO date (YYYY-MM-DD). Important to divide every variable with empty space");

            string searchQuery = Console.ReadLine();

            string fileName = searchQuery.Split(" ")[0];
            string outDataFilePath = searchQuery.Split(" ")[1];
            string isoDate = searchQuery.Split(" ")[2];

            List<Transaction> allTransactions = new List<Transaction>();
            var path = Directory.GetCurrentDirectory() + @"\data\" + fileName;

            List<string> values = File.ReadAllLines(path).ToList();

            for (int i = 1; i < values.Count; i++)
            {

                Transaction singleTran = new Transaction();
                allTransactions.Add(new Transaction
                {
                    konto = Int32.Parse(values[i].Split(";")[0]),
                    belopp = values[i].Split(";")[1],
                    date = values[i].Split(";")[2],
                    bank = values[i].Split(";")[3]
                });
            }


            if (Directory.Exists(outDataFilePath))
            {
                List<string> banks = new List<string>();
                foreach (var item in allTransactions)
                {
                    if (!banks.Contains(item.bank))
                    {
                        var items = allTransactions.Where(x => x.bank == item.bank).ToList();


                        List<string> textInformation = new List<string>() { @"Konto;Belopp;Datum;Typ" };
                        foreach (var row in items)
                        {
                            textInformation.Add(row.konto + ";" + row.belopp + ";" + row.date + ";" + GetDateStatus(row.date, isoDate));
                        }

                        File.WriteAllLines(outDataFilePath +"\\" + item.bank+".txt", textInformation);
                        banks.Add(item.bank);
                    }
                }
            }
            else
            {
                Console.WriteLine("The filepath does not exist.");
            }

            //    /////////////////////////////////////////
            string GetDateStatus(string rowDate, string isoDate)
            {
                
                DateTime _rowDate = DateTime.Parse(rowDate);
                DateTime _isoDate = DateTime.Parse(isoDate);
                var condition = DateTime.Compare(_rowDate, _isoDate);

                switch (condition)
                {
                    case -1:
                        return "OLD";
                    case 0:
                        return "ACTIVE";
                    case 1:
                        return "FUTURE";
                    default:
                        return "unvalid date format";
                }
            }

        }


    }
    }
