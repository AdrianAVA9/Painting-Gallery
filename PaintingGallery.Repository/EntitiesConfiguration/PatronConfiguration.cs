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
                    new[]
                    {
                        new System.ComponentModel.DataAnnotations.Schema.IndexAttribute("Identification")
                        {
                            IsUnique = true
                        }
                    })
                );

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
                .HasMaxLength(100);

            Property(p => p.Death)
                .IsRequired();
        }
    }
}
