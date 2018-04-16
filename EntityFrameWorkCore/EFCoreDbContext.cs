using Data;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWorkCoreTest
{
	public class EfCoreDbContext : DbContext
	{
		public EfCoreDbContext(DbContextOptions<EfCoreDbContext> contextOptions)
			: base(contextOptions)
		{
		}

		public DbSet<Document> Documents { get; set; }
		public DbSet<OtherDocument> OtherDocuments { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Document>(d =>
			{
				d.ToTable(nameof(Document));
				d.OwnsOne(x => x.PublicationEvent).Property(w => w.Date).HasColumnName("PublicationEvent_Date");
				d.OwnsOne(x => x.PublicationEvent).Property(w => w.UserId).HasColumnName("PublicationEvent_UserId");
				d.Property(w => w.Name).HasColumnName("Name");
				d.HasKey(x => x.Id);
				d.HasDiscriminator<DocumentType>(nameof(DocumentType))
					.HasValue<Document>(DocumentType.BaseDocument)
					.HasValue<OtherDocument>(DocumentType.OtherDocument);
			});

			builder.Entity<OtherDocument>().ToTable(nameof(OtherDocument));

			base.OnModelCreating(builder);
		}
	}
}