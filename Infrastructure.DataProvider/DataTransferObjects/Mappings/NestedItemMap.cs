using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class NestedItemMap : IEntityTypeConfiguration<NestedItemDto>
    {
        public void Configure(EntityTypeBuilder<NestedItemDto> builder)
        {
            builder.Property(p => p.Id).UseSqlServerIdentityColumn();
            builder.HasKey(w => w.Id).ForSqlServerIsClustered();
            builder.HasIndex(e => e.OtherDocumentItemId);
            builder.HasOne(w => w.OtherDocumentItemDto)
                .WithMany(w => w.NestedItemDtos)
                .HasForeignKey(w => w.OtherDocumentItemId);
            builder.ToTable("NestedItem");
        }
    }
}