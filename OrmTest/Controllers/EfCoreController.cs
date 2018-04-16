using System.Threading.Tasks;
using Data;
using EntityFrameWorkCoreTest;
using Microsoft.AspNetCore.Mvc;

namespace OrmTest.Controllers
{
	[Route("api/[controller]")]
	public class EfCoreController : BaseController
	{
		private readonly EntityFrameworkCoreRepository _repository;

		public EfCoreController(EfCoreDbContext dbContext)
		{
			_repository = new EntityFrameworkCoreRepository(dbContext);
		}

		[HttpGet("documents")]
		public async Task<IActionResult> Index()
		{
			var documents = await _repository.GetDocumentsAsync();
			return Ok(documents);
		}

		[HttpGet("otherDocuments")]
		public async Task<IActionResult> Other()
		{
			var documents = await _repository.GetOtherDocumentsAsync();
			return Ok(documents);
		}

		[HttpGet("create")]
		public async Task<IActionResult> CreateDocument(Document document)
		{
			var doc = await _repository.CreateAsync(document);
			return Ok(doc);
		}
	}
}