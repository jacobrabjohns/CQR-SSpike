using System;
using System.Threading.Tasks;
using CQRSSpike.Commands;
using CQRSSpike.Models;
using MediatR;

namespace CQRSSpike.Handlers
{
    public class WithdrawFromCustomerAccountCommandHandler :
        IAsyncRequestHandler<WithdrawFromCustomerAccountCommand, Unit>
    {
        private readonly AccountsContext _context;

        public WithdrawFromCustomerAccountCommandHandler(AccountsContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(WithdrawFromCustomerAccountCommand message)
        {
            var customer = await _context.Customers.FindAsync(message.ID);
            customer.WithdrawMoney(message.Amount);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}