namespace epicnetwork.rocks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Merchants",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        formatted_address = c.String(),
                        formatted_phone_number = c.String(),
                        image = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        week = c.Int(nullable: false),
                        title = c.String(),
                        description = c.String(),
                        image = c.String(),
                        value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        merchant_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Merchants", t => t.merchant_id)
                .Index(t => t.merchant_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promotions", "merchant_id", "dbo.Merchants");
            DropIndex("dbo.Promotions", new[] { "merchant_id" });
            DropTable("dbo.Promotions");
            DropTable("dbo.Merchants");
        }
    }
}
