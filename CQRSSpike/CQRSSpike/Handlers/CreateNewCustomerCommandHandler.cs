using System;
using System.Threading.Tasks;
using CQRSSpike.Commands;
using CQRSSpike.Models;
using MediatR;

namespace CQRSSpike.Handlers
{
    public class CreateNewCustomerCommandHandler : IAsyncRequestHandler<CreateNewCustomerCommand,Unit>
    {
        private readonly AccountsContext _accountsContext;

        public CreateNewCustomerCommandHandler(AccountsContext context)
        {
            _accountsContext = context;
        }

        public async Task<Unit> Handle(CreateNewCustomerCommand message)
        {
            var customer = new Customer(message);
            var account = new BankAccount(message.StartingBalance, Currency.GBP);
            customer.AccountID = account.AccountNumber;


            _accountsContext.Accounts.Add(account);
            _accountsContext.Customers.Add(customer);
            await _accountsContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}