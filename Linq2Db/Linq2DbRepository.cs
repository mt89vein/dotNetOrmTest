using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using LinqToDB;

namespace Linq2Db
{
	public class Linq2DbRepository
	{
		private readonly Linq2DbContext _ctx;

		public Linq2DbRepository(Linq2DbContext ctx)
		{
			_ctx = ctx;
		}

		public async Task<List<Document>> GetDocumentsAsync()
		{
			return await _ctx.Documents.ToListAsync();
		}

		public async Task<List<OtherDocument>> GetOtherDocumentsAsync()
		{
			return await _ctx.OtherDocuments.ToListAsync();
		}

		public async Task<Document> GetAsync(int id)
		{
			return await _ctx.Documents.FirstOrDefaultAsync(w => w.Id == id);
		}

		public async Task<Document> CreateAsync(Document document)
		{
			var docTask = await _ctx.Documents
				.Value(w => w.PublicationEvent, document.PublicationEvent)
				.Value(w => w.Name, document.Name)
				.Value(w => w.DocumentType, document.DocumentType)
				.InsertAsync();

			document.Id = docTask;
			return document;
		}

		public async Task UpdateAsync(Document document)
		{
			await _ctx.Documents.Where(w => w.Id == document.Id)
				.Set(w => w.PublicationEvent, document.PublicationEvent)
				.Set(w => w.Name, document.Name)
				.Set(w => w.DocumentType, document.DocumentType)
				.UpdateAsync();
		}

		public async Task DeleteAsync(int id)
		{
			await _ctx.Documents.Where(w => w.Id == id).DeleteAsync();
		}
	}
}