using System.Collections.Generic;
using System.Linq;
using Data;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace Linq2Db
{
	public class ConnectionStringSettings : IConnectionStringSettings
	{
		public string ConnectionString { get; set; }
		public string Name { get; set; }
		public string ProviderName { get; set; }
		public bool IsGlobal => false;
	}

	public class MySettings : ILinqToDBSettings
	{
		public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

		public string DefaultConfiguration => "OrmTest";
		public string DefaultDataProvider => "SqlServer";

		public IEnumerable<IConnectionStringSettings> ConnectionStrings
		{
			get
			{
				yield return
					new ConnectionStringSettings
					{
						Name = "OrmTest",
						ProviderName = "SqlServer",
						ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OrmTest;Integrated Security=True;"
					};
			}
		}
	}

	public class Linq2DbContext : DataConnection
	{
		public Linq2DbContext() : base("OrmTest")
		{
			var documentBuilder = MappingSchema.GetFluentMappingBuilder().Entity<Document>();
			documentBuilder.HasAttribute(new InheritanceMappingAttribute
			{
				Code = DocumentType.BaseDocument,
				Type = typeof(Document),
			});
			documentBuilder.HasAttribute(new InheritanceMappingAttribute
			{
				Code = DocumentType.OtherDocument,
				Type = typeof(OtherDocument),
			});
			documentBuilder.HasTableName(nameof(Document));
			documentBuilder.Property(x => x.DocumentType).IsDiscriminator();
			documentBuilder.Property(x => x.Id).IsPrimaryKey();
			documentBuilder.Property(x => x.Name);
			documentBuilder.Property(x => x.PublicationEvent.Date).HasColumnName("PublicationEvent_Date");
			documentBuilder.Property(x => x.PublicationEvent.UserId).HasColumnName("PublicationEvent_UserId");

			documentBuilder.Entity<OtherDocument>().HasTableName(nameof(OtherDocument));
		}

		public ITable<Document> Documents => GetTable<Document>();
		public ITable<OtherDocument> OtherDocuments => GetTable<OtherDocument>();
	}
}