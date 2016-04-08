using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper.QueryableExtensions;
using CQRSSpike.Commands;
using CQRSSpike.Models;
using MediatR;

namespace CQRSSpike.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly AccountsContext _accountsContext;
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator, AccountsContext context)
        {
            _accountsContext = context;
            _mediator = mediator;
        }

        /// <summary>
        ///     Create a new customer, pre-providing the ID so as to query with it later.
        /// </summary>
        /// <param name="id">The guid to be used as the new ID. Persist this to make further queries</param>
        /// <param name="startingBalance">The initial balance</param>
        /// <param name="firstName">Customers first name</param>
        /// <param name="lastName">Customers last name</param>
        /// <returns>Get the account balance to see the new customer</returns>
        [HttpPost]
        public async Task<IHttpActionResult> CreateNewCustomer(Guid id, decimal startingBalance, string firstName,
            string lastName)
        {
            await _mediator.SendAsync(new CreateNewCustomerCommand(id, startingBalance, firstName, lastName));
            return Ok();
        }


        /// <summary>
        ///     Get a CustomerBankBalance model representing the customers bank balance at that time
        /// </summary>
        /// <param name="id">The GUID of the customer whos account details you wish to retrieve</param>
        /// <returns>CustomerBankBalance</returns>
        [ResponseType(typeof (CustomerBankBalance))]
        public IHttpActionResult GetCustomerBalance(Guid id)
        {
            return Ok(_accountsContext.Customers.Where(x => x.ID == id).ProjectTo<CustomerBankBalance>().Single());
        }

        /// <summary>
        ///     Subtracts the requested sum from the specified account balance
        /// </summary>
        /// <param name="id">The GUID of the customer whos account details you wish to retrieve</param>
        /// <param name="value">The decimal amount of money to withdraw</param>
        /// <returns>Get the account balance to see the updated value</returns>
        [HttpPost]
        public async Task<IHttpActionResult> WithdrawFromCustomerAccount(Guid id, decimal value)
        {
            await _mediator.SendAsync(new WithdrawFromCustomerAccountCommand(id, value));
            return Ok();
        }

        /// <summary>
        ///     Adds the requested sum to the specified account balance
        /// </summary>
        /// <param name="id">The GUID of the customer whos account details you wish to retrieve</param>
        /// <param name="value">The decimal amount of money to deposit</param>
        /// <returns>Get the account balance to see the updated value</returns>
        [HttpPost]
        public async Task<IHttpActionResult> DepositIntoCustomerAccount(Guid id, decimal value)
        {
            await _mediator.SendAsync(new DepositIntoCustomerAccountCommand(id, value));
            return Ok();
        }

        [HttpPost]
        public Task<IHttpActionResult> OpenThePodDoorsHal()
        {
            throw new Exception("Oh noes!");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _accountsContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}