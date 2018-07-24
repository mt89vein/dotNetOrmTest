using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
	internal class OtherDocumentPaymentMap : IEntityTypeConfiguration<OtherDocumentPaymentDto>
	{
		public void Configure(EntityTypeBuilder<OtherDocumentPaymentDto> builder)
		{
			builder.Property(w => w.Id);
			builder.HasKey(w => w.Id);
			builder.HasOne(w => w.OtherDocumentDto).WithMany(w => w.OtherDocumentPaymentDtos)
				.HasForeignKey(w => w.OtherDocumentId);
			builder.ToTable("OtherDocumentPayment");
		}
	}
}