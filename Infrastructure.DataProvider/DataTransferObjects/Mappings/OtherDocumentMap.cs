using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class OtherDocumentMap : IEntityTypeConfiguration<OtherDocumentDto>
    {
        public void Configure(EntityTypeBuilder<OtherDocumentDto> builder)
        {
            builder.HasKey(w => w.Id).ForSqlServerIsClustered();
            builder.Property(w => w.Id).ValueGeneratedNever().IsRequired();
            builder.Metadata.FindNavigation(nameof(OtherDocumentDto.DocumentDto)).IsEagerLoaded = true;
            builder.HasMany(w => w.OtherDocumentItemDtos).WithOne(w => w.OtherDocumentDto)
                .HasForeignKey(w => w.OtherDocumentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(w => w.OtherDocumentPaymentDtos).WithOne(w => w.OtherDocumentDto)
                .HasForeignKey(w => w.OtherDocumentId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("OtherDocument");
        }
    }
}