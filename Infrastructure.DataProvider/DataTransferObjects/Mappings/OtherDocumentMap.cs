using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class OtherDocumentMap : IEntityTypeConfiguration<OtherDocumentDto>
    {
        public void Configure(EntityTypeBuilder<OtherDocumentDto> builder)
        {
            builder.Property(w => w.Id).ValueGeneratedNever();
            builder.HasKey(w => w.Id);
            builder.OwnsOne(w => w.PublicationEvent).Property(w => w.Date);
            builder.OwnsOne(w => w.PublicationEvent).Property(w => w.UserId);
            builder.HasMany(w => w.OtherDocumentItemDtos).WithOne(w => w.OtherDocumentDto)
                .HasForeignKey(w => w.OtherDocumentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(w => w.OtherDocumentPaymentDtos).WithOne(w => w.OtherDocumentDto)
                .HasForeignKey(w => w.OtherDocumentId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("OtherDocument");
		}
    }
}