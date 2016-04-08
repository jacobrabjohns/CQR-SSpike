using System;
using MediatR;

namespace CQRSSpike.Commands
{
    public class CreateNewCustomerCommand : IAsyncRequest
    {
        public CreateNewCustomerCommand(Guid id, decimal startingBalance, string firstName, string lastName)
        {
            ID = id;
            StartingBalance = startingBalance;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid ID { get; }
        public decimal StartingBalance { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}