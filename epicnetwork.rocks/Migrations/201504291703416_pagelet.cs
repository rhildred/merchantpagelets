namespace epicnetwork.rocks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pagelet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pagelets",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        value = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pagelets");
        }
    }
}
