using PaintingGallery.Repository.Entities;
using PaintingGallery.Repository.EntitiesConfiguration;
using System.Data.Entity;

namespace PaintingGallery.Repository
{
    public class PaintingGalleryContext : DbContext
    {
        public DbSet<Patron> Patrons { get; set; }

        public PaintingGalleryContext(string connetionString) : base(connetionString)
        {
        }

        public PaintingGalleryContext() : base("PaintingGalleryConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PatronConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
