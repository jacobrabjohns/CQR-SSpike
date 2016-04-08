namespace CQRSSpike.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccount", "StoredCurrency", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankAccount", "StoredCurrency");
        }
    }
}
