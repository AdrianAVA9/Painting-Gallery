using PaintingGallery.Repository.Entities;
using PaintingGallery.Repository.EntitiesConfiguration;
using System.Data.Entity;

namespace PaintingGallery.Repository
{
    public class PaintingGalleryContext : DbContext
    {
        public DbSet<Patron> Patrons { get; set; }

<<<<<<< HEAD
        public PaintingGalleryContext(string connetionString) : base(connetionString)
        {
        }

        public PaintingGalleryContext() : base("PaintingGalleryConnection")
        {
        }

=======
>>>>>>> 21ba783075123c3610b1f1dab7f6579cf394c4f0
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PatronConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
