using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class DocumentMap : IEntityTypeConfiguration<DocumentDto>
    {
        public void Configure(EntityTypeBuilder<DocumentDto> builder)
        {
            builder.Property(w => w.Id).ValueGeneratedNever();
            builder.HasOne(w => w.OtherDocumentDto).WithOne(w => w.DocumentDto)
                .HasForeignKey<OtherDocumentDto>(w => w.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(w => w.SecondDocumentDto).WithOne(w => w.DocumentDto)
                .HasForeignKey<SecondDocumentDto>(w => w.Id).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Document");
        }
    }
}