using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class OtherDocumentItemMap : IEntityTypeConfiguration<OtherDocumentItemDto>
    {
        public void Configure(EntityTypeBuilder<OtherDocumentItemDto> builder)
        {
            builder.Property(p => p.Id).UseSqlServerIdentityColumn();
            builder.HasKey(w => w.Id).ForSqlServerIsClustered();
            builder.HasIndex(e => e.OtherDocumentId);
            builder.HasOne(d => d.OtherDocumentDto)
                .WithMany(p => p.OtherDocumentItemDtos)
                .HasForeignKey(d => d.OtherDocumentId);
            builder.ToTable("OtherDocumentItem");
        }
    }
}