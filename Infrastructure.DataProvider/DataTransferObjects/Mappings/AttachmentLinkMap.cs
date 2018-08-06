using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class AttachmentLinkMap : IEntityTypeConfiguration<AttachmentLinkDto>
    {
        public void Configure(EntityTypeBuilder<AttachmentLinkDto> builder)
        {
            builder.Property(w => w.Id).ValueGeneratedOnAdd().UseSqlServerIdentityColumn();
            builder.HasOne(w => w.AttachmentDto).WithMany(w => w.AttachmentLinkDtos)
                .HasForeignKey(w => w.AttachmentId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(w => w.DocumentDto).WithMany(w => w.AttachmentLinkDtos)
                .HasForeignKey(w => w.DocumentId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("AttachmentLink");
        }
    }
}