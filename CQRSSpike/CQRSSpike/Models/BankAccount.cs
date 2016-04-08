using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQRSSpike.Models
{
    [Table("BankAccount")]
    public class BankAccount
    {
        public BankAccount()
        {
        }

        public BankAccount(decimal startingBalance, Currency currency)
        {
            AccountNumber = Guid.NewGuid();
            Balance = new Money(startingBalance, currency);
        }

        [Key]
        public Guid AccountNumber { get; set; }
        public Money Balance { get; set; }


        public void Debit(decimal value)
        {
            if (value < Balance.Amount)
            {
                Balance.Subtract(value);
            }
            else
            {
                throw new Exception("Cannot debit an amount greater than the balance");
            }
        }

        public void Credit(decimal value)
        {
            Balance.Add(value);
        }
    }

    public enum Currency
    {
        GBP,
        USD
    }
}