namespace PaintingGallery.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patrons", "PicturePath", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Patrons", "Birthdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patrons", "Birthdate");
            DropColumn("dbo.Patrons", "PicturePath");
        }
    }
}
