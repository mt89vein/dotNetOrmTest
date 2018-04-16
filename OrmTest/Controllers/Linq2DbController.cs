using System.Threading.Tasks;
using Data;
using Linq2Db;
using Microsoft.AspNetCore.Mvc;

namespace OrmTest.Controllers
{
	[Route("api/[controller]")]
	public class Linq2DbController : BaseController
	{
		private readonly Linq2DbRepository _repository;

		public Linq2DbController()
		{
			_repository = new Linq2DbRepository(new Linq2DbContext());
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