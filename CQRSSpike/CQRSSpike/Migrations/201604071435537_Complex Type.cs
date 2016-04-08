namespace CQRSSpike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccount", "Currency", c => c.Int(nullable: false));
            DropColumn("dbo.BankAccount", "StoredCurrency");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccount", "StoredCurrency", c => c.Int(nullable: false));
            DropColumn("dbo.BankAccount", "Currency");
        }
    }
}
