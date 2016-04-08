using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSSpike.Models
{
    [ComplexType]
    public class Money
    {

        public Money()
        {

        }

        public Money(decimal amount, Currency storedCurrency)
        {
            StoredCurrency = storedCurrency;
            Amount = amount;

        }


        public decimal Amount { get; set; } // Base GBP, assume dollars are worth 1/2
        public Currency StoredCurrency { get; set; }


        public void Add(decimal value)
        {
            Amount += value;
        }

        public void Subtract(decimal value)
        {
            Amount -= value;
        }

    }
}