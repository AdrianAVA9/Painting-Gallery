namespace PaintingGallery.Repository.EntitiesConfiguration
{
    public class PatronConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<PaintingGallery.Repository.Entities.Patron>
    {
        public PatronConfiguration()
        {
            Property(p => p.Identification)
                .IsRequired()
                .HasColumnAnnotation("Identification",
                new System.Data.Entity.Infrastructure.Annotations.IndexAnnotation
                (
<<<<<<< HEAD
                    new System.ComponentModel.DataAnnotations.Schema.IndexAttribute("Identification")
                    {
                        IsUnique = true
                    })
                );

            //HasIndex(p => p.Identification)
            //    .IsUnique();

            //Property(p => p.Identification)
            //    .IsRequired()
            //    .HasMaxLength(15);

=======
                    new[]
                    {
                        new System.ComponentModel.DataAnnotations.Schema.IndexAttribute("Identification")
                        {
                            IsUnique = true
                        }
                    })
                );

>>>>>>> 21ba783075123c3610b1f1dab7f6579cf394c4f0
            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(80);

            Property(p => p.Country)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.City)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.Birthdate)
                .IsRequired();

            Property(p => p.Picture)
                .IsRequired()
<<<<<<< HEAD
                .HasColumnName("PicturePath")
=======
>>>>>>> 21ba783075123c3610b1f1dab7f6579cf394c4f0
                .HasMaxLength(100);

            Property(p => p.Death)
                .IsRequired();
        }
    }
}
