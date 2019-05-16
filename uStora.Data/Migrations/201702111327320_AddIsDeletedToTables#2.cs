namespace uStora.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedToTables2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.PostCategories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Posts", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "IsDeleted");
            DropColumn("dbo.PostCategories", "IsDeleted");
            DropColumn("dbo.ProductCategories", "IsDeleted");
        }
    }
}
