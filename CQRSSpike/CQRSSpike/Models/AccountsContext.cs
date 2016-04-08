using System.Data.Entity;

namespace CQRSSpike.Models
{
    public class AccountsContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public AccountsContext() : base("name=AccountsContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<BankAccount> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types<BankAccount>()
                .Configure(ctc => ctc.Property(x => x.Balance.Amount).HasColumnName("Amount"));
            modelBuilder.Types<BankAccount>()
                .Configure(ctc => ctc.Property(x => x.Balance.StoredCurrency).HasColumnName("Currency"));


        }
    }
}