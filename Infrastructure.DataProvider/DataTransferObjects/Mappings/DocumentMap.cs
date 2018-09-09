using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class DocumentMap : IEntityTypeConfiguration<DocumentDto>
    {
        public void Configure(EntityTypeBuilder<DocumentDto> builder)
        {
            builder.HasKey(w => w.Id).ForSqlServerIsClustered();
            builder.Property(w => w.Id).UseSqlServerIdentityColumn();
            builder.HasOne(w => w.OtherDocumentDto)
                .WithOne(w => w.DocumentDto)
                .HasForeignKey<OtherDocumentDto>(w => w.Id)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(w => w.AttachmentLinkDtos)
                .WithOne(w => w.DocumentDto)
                .HasForeignKey(w => w.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Document");
        }
    }
}