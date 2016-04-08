using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CQRSSpike.Commands;

namespace CQRSSpike.Models
{
    public class Customer
    {
        public Customer()
        {
        }

        public Customer(CreateNewCustomerCommand command)
        {
            ID = command.ID;
            FirstName = command.FirstName;
            LastName = command.LastName;
        }

        [Key]
        public Guid ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [ForeignKey(nameof(Account))]
        public Guid AccountID { get; set; }

        public virtual BankAccount Account { get; set; }

        public void WithdrawMoney(decimal value)
        {
            Account.Debit(value);
        }

        public void GetPaid(decimal value)
        {
            Account.Credit( value);
        }
    }
}