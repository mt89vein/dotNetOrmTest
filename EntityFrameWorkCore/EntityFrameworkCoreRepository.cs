using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWorkCoreTest
{
	public class EntityFrameworkCoreRepository
	{
		private readonly EfCoreDbContext _ctx;

		public EntityFrameworkCoreRepository(EfCoreDbContext ctx)
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
			return await _ctx.Documents.FindAsync(id);
		}

		public async Task<Document> CreateAsync(Document document)
		{
			var docTask = await _ctx.Documents.AddAsync(document);
			return docTask.Entity;
		}

		public async Task UpdateAsync(Document document)
		{
			var documentDto = await _ctx.Documents.FindAsync(document.Id);

			_ctx.Documents.Update(documentDto);
		}

		public async Task DeleteAsync(int id)
		{
			var documentDto = await _ctx.Documents.FindAsync(id);

			_ctx.Documents.Remove(documentDto);
		}
	}
}