namespace OnlineShopping.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Products", "CategoriesID", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CategoriesID");
            AddForeignKey("dbo.Products", "CategoriesID", "dbo.Categories", "ID", cascadeDelete: true);
            DropColumn("dbo.Products", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CategoryId", c => c.Int());
            DropForeignKey("dbo.Products", "CategoriesID", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoriesID" });
            DropColumn("dbo.Products", "CategoriesID");
            DropTable("dbo.Categories");
          
        }
    }
}
