namespace OnlineShopping.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productsPictures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        ProductName = c.String(),
                        CategoryId = c.Int(),
                        IsActive = c.Boolean(),
                        IsDelete = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Description = c.String(),
                        IsFeatured = c.Boolean(),
                        Quantity = c.Int(),
                        Price = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                        Products_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Products_ID)
                .Index(t => t.PictureID)
                .Index(t => t.Products_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductPictures", "Products_ID", "dbo.Products");
            DropForeignKey("dbo.ProductPictures", "PictureID", "dbo.Pictures");
            DropIndex("dbo.ProductPictures", new[] { "Products_ID" });
            DropIndex("dbo.ProductPictures", new[] { "PictureID" });
            DropTable("dbo.ProductPictures");
            DropTable("dbo.Products");
        }
    }
}
