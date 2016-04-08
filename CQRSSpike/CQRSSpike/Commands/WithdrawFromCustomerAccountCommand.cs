using System;
using MediatR;

namespace CQRSSpike.Commands
{
    public class WithdrawFromCustomerAccountCommand : IAsyncRequest
    {
        public WithdrawFromCustomerAccountCommand(Guid id, decimal amount)
        {
            ID = id;
            Amount = amount;
        }

        public Guid ID { get; }
        public decimal Amount { get; }
    }
}