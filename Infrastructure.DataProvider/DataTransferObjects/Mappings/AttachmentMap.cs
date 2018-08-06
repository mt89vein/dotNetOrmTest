using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class AttachmentMap : IEntityTypeConfiguration<AttachmentDto>
    {
        public void Configure(EntityTypeBuilder<AttachmentDto> builder)
        {
            builder.Property(w => w.Id).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            builder.HasMany(w => w.AttachmentLinkDtos).WithOne(w => w.AttachmentDto)
                .HasForeignKey(w => w.AttachmentId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Attachment");
        }
    }
}