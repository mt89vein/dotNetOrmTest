using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Data;

namespace DapperTest
{
	public class DapperRepository
	{
		private readonly string _connectionString;

		public DapperRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<List<Document>> GetDocumentsAsync()
		{
			List<Document> documents;

			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				const string sql = "SELECT Id, DocumentType, Name, PublicationEvent_Date as Date, PublicationEvent_UserId as UserId FROM document";
				var docs = await db.QueryAsync<Document, PublicationEvent, Document>(
					sql,
					(document, publicationEvent) =>
					{
						document.PublicationEvent = publicationEvent;

						return document;
					}, splitOn: "Date");

				documents = docs.ToList();
			}

			return documents;
		}

		public async Task<List<OtherDocument>> GetOtherDocumentsAsync()
		{
			List<OtherDocument> documents;

			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				const string sql =
					"SELECT d.Id, d.DocumentType, d.Name, od.TestName, d.PublicationEvent_Date as Date, d.PublicationEvent_UserId as UserId FROM document d JOIN OtherDocument od ON d.Id = od.Id ";
				var docs = await db.QueryAsync<OtherDocument, PublicationEvent, OtherDocument>(sql,
					(document, publicationEvent) =>
					{
						document.PublicationEvent = publicationEvent;
						return document;
					}, splitOn: "Date");

				documents = docs.ToList();
			}

			return documents;
		}

		public async Task<Document> GetAsync(int id)
		{
			Document document;
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				const string sqlQuery = "SELECT * FROM Document WHERE Id = @id";
				document = await db.QueryFirstAsync<Document>(sqlQuery, new {id});
			}
			return document;
		}

		public async Task<Document> CreateAsync(Document document)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				const string sqlQuery =
					@"INSERT INTO Document
						(Name, PublicationEvent_Date, PublicationEvent_UserId, DocumentType) 
						VALUES (@Name, @PublicationEvent_Date, @PublicationEvent_UserId, @DocumentType); 
						SELECT CAST(SCOPE_IDENTITY() as int)";
				document.Id = await db.QueryFirstAsync<int>(sqlQuery, document);
			}
			return document;
		}

		public async Task UpdateAsync(Document document)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				const string sqlQuery =
					@"UPDATE Document SET 
						Name = @Name, 
						PublicationEvent_Date = @PublicationEvent_Date, 
						PublicationEvent_UserId = @PublicationEvent_UserId
						DocumentType = @DocumentType
					  WHERE Id = @Id";
				await db.QueryAsync(sqlQuery, document);
			}
		}

		public async Task DeleteAsync(int id)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				const string sqlQuery = "DELETE FROM Document WHERE Id = @id";
				await db.QueryAsync(sqlQuery, new {id});
			}
		}
	}
}