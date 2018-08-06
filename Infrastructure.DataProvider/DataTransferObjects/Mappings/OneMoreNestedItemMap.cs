using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class OneMoreNestedItemMap : IEntityTypeConfiguration<OneMoreNestedItemDto>
    {
        public void Configure(EntityTypeBuilder<OneMoreNestedItemDto> builder)
        {
            builder.Property(w => w.Id);
            builder.HasKey(w => w.Id);
            builder.HasOne(w => w.NestedItemDto).WithMany(w => w.OneMoreNestedItemDtos)
                .HasForeignKey(w => w.NestedItemId);
            builder.ToTable("OneMoreNestedItem");
        }
    }
}