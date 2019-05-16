namespace uStora.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedToTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationGroups", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationRoles", "IsDeleted", c => c.Boolean(nullable: true));
            AddColumn("dbo.ApplicationUsers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Brands", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Vehicles", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "IsDeleted");
            DropColumn("dbo.Products", "IsDeleted");
            DropColumn("dbo.Brands", "IsDeleted");
            DropColumn("dbo.ApplicationUsers", "IsDeleted");
            DropColumn("dbo.ApplicationRoles", "IsDeleted");
            DropColumn("dbo.ApplicationGroups", "IsDeleted");
        }
    }
}
