using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class AttachmentMap : IEntityTypeConfiguration<AttachmentDto>
    {
        public void Configure(EntityTypeBuilder<AttachmentDto> builder)
        {
            builder.Property(p => p.Id).UseSqlServerIdentityColumn();
            builder.HasKey(w => w.Id).ForSqlServerIsClustered();
            builder.HasMany(w => w.AttachmentLinkDtos).WithOne(w => w.AttachmentDto)
                .HasForeignKey(w => w.AttachmentId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Attachment");
        }
    }
}