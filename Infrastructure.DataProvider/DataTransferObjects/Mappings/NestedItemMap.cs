using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class NestedItemMap : IEntityTypeConfiguration<NestedItemDto>
    {
        public void Configure(EntityTypeBuilder<NestedItemDto> builder)
        {
            builder.Property(w => w.Id);
            builder.HasKey(w => w.Id);
            builder.HasOne(w => w.OtherDocumentItemDto).WithMany(w => w.NestedItemDtos)
                .HasForeignKey(w => w.OtherDocumentItemId);
            builder.ToTable("NestedItem");
        }
    }
}