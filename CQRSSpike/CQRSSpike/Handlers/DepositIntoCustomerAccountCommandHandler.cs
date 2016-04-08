using System;
using System.Threading.Tasks;
using CQRSSpike.Commands;
using CQRSSpike.Models;
using MediatR;

namespace CQRSSpike.Handlers
{
    public class DepositIntoCustomerAccountCommandHandler : IAsyncRequestHandler<DepositIntoCustomerAccountCommand, Unit>
    {
        private readonly AccountsContext _context;

        public DepositIntoCustomerAccountCommandHandler(AccountsContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DepositIntoCustomerAccountCommand message)
        {
            var customer = await _context.Customers.FindAsync(message.ID);
            customer.GetPaid(message.Amount);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}