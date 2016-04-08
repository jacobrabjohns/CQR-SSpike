namespace CQRSSpike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BankAccounts", newName: "BankAccount");
            DropColumn("dbo.BankAccount", "Balance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccount", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            RenameTable(name: "dbo.BankAccount", newName: "BankAccounts");
        }
    }
}
