using System;
using MediatR;

namespace CQRSSpike.Commands
{
    public class DepositIntoCustomerAccountCommand : IAsyncRequest
    {
        public DepositIntoCustomerAccountCommand(Guid id, decimal amount)
        {
            ID = id;
            Amount = amount;
        }

        public Guid ID { get; }
        public decimal Amount { get; }
    }
}