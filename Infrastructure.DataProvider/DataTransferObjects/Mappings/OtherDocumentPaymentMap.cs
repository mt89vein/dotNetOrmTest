using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class OtherDocumentPaymentMap : IEntityTypeConfiguration<OtherDocumentPaymentDto>
    {
        public void Configure(EntityTypeBuilder<OtherDocumentPaymentDto> builder)
        {
            builder.Property(p => p.Id).UseSqlServerIdentityColumn();
            builder.HasKey(w => w.Id).ForSqlServerIsClustered();
            builder.HasIndex(e => e.OtherDocumentId);
            builder.HasOne(d => d.OtherDocumentDto)
                .WithMany(p => p.OtherDocumentPaymentDtos)
                .HasForeignKey(d => d.OtherDocumentId);
            builder.ToTable("OtherDocumentPayment");
        }
    }
}