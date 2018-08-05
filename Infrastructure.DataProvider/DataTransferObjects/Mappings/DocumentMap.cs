using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class DocumentMap : IEntityTypeConfiguration<DocumentDto>
    {
        public void Configure(EntityTypeBuilder<DocumentDto> builder)
        {
            builder.Property(w => w.Id).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            builder.HasOne(w => w.OtherDocumentDto).WithOne(w => w.DocumentDto)
                .HasForeignKey<OtherDocumentDto>(w => w.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(w => w.AttachmentDtos).WithOne(w => w.DocumentDto)
                .HasForeignKey(w => w.DocumentId).OnDelete(DeleteBehavior.SetNull);
            builder.ToTable("Document");
        }
    }
}