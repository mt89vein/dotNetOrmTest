using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class OneMoreNestedItemMap : IEntityTypeConfiguration<OneMoreNestedItemDto>
    {
        public void Configure(EntityTypeBuilder<OneMoreNestedItemDto> builder)
        {
            builder.Property(p => p.Id).UseSqlServerIdentityColumn();
            builder.HasKey(w => w.Id).ForSqlServerIsClustered();
            builder.HasIndex(e => e.NestedItemId);
            builder.HasOne(d => d.NestedItemDto)
                .WithMany(p => p.OneMoreNestedItemDtos)
                .HasForeignKey(d => d.NestedItemId);
            builder.ToTable("OneMoreNestedItem");
        }
    }
}