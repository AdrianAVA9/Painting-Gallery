namespace PaintingGallery.Repository.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patrons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Identification = c.String(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "Identification",
                                    new AnnotationValues(oldValue: null, newValue: "IndexAnnotation: { Name: Identification, IsUnique: True }")
                                },
                            }),
                        Name = c.String(nullable: false, maxLength: 80),
                        Country = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        Death = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Patrons",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "Identification",
                        new Dictionary<string, object>
                        {
                            { "Identification", "IndexAnnotation: { Name: Identification, IsUnique: True }" },
                        }
                    },
                });
        }
    }
}
