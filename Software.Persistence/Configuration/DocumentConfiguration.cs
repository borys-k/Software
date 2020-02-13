using Software.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Software.Persistence.Configuration
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Document");

            builder.HasKey(e => e.DocumentId);

            builder.Property(e => e.DocumentId)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Content)
                .IsRequired();

            builder.Property(e => e.FileSize)
                .IsRequired();
        }
    }
}
