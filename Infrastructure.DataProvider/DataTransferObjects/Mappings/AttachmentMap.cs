using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
	internal class AttachmentMap : IEntityTypeConfiguration<AttachmentDto>
	{
		public void Configure(EntityTypeBuilder<AttachmentDto> builder)
		{
			builder.Property(w => w.Id).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
			builder.HasOne(w => w.DocumentDto).WithMany(w => w.AttachmentDtos)
				.HasForeignKey(w => w.DocumentId);
			builder.ToTable("Attachment");
		}
	}
}