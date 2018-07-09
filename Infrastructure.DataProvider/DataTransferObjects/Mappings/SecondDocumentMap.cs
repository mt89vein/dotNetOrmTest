using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataProvider.Mappings
{
    internal class SecondDocumentMap : IEntityTypeConfiguration<SecondDocumentDto>
    {
        public void Configure(EntityTypeBuilder<SecondDocumentDto> builder)
        {
            builder.Property(w => w.Id).ValueGeneratedNever();
            builder.HasKey(w => w.Id);
            builder.ToTable("SecondDocument");
        }
    }
}