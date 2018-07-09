using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class OtherDocumentItemMap : IEntityTypeConfiguration<OtherDocumentItemDto>
    {
        public void Configure(EntityTypeBuilder<OtherDocumentItemDto> builder)
        {
            builder.Property(w => w.Id);
            builder.HasKey(w => w.Id);
            builder.HasOne(w => w.OtherDocumentDto).WithMany(w => w.OtherDocumentItemDtos)
                .HasForeignKey(w => w.OtherDocumentId);
            builder.ToTable("OtherDocumentItem");
        }
    }
}