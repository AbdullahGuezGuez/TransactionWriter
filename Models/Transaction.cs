using System;
using System.Collections.Generic;
using System.Text;

namespace BankCalculator.Models
{
    public class Transaction
    {
        public int konto { get; set; }
        public string belopp { get; set; }

        public string date { get; set; }
        public string bank { get; set; }
    }
}


