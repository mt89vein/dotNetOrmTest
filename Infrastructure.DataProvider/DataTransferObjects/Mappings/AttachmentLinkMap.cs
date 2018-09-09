using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class AttachmentLinkMap : IEntityTypeConfiguration<AttachmentLinkDto>
    {
        public void Configure(EntityTypeBuilder<AttachmentLinkDto> builder)
        {
            builder.Property(p => p.Id).UseSqlServerIdentityColumn();
            builder.HasKey(w => w.Id).ForSqlServerIsClustered();
            builder.HasIndex(e => e.AttachmentId);
            builder.HasIndex(e => e.DocumentId);
            builder.HasOne(d => d.AttachmentDto)
                .WithMany(p => p.AttachmentLinkDtos)
                .HasForeignKey(d => d.AttachmentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(d => d.DocumentDto)
                .WithMany(p => p.AttachmentLinkDtos)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("AttachmentLink");
        }
    }
}