using System.Threading.Tasks;
using DapperTest;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace OrmTest.Controllers
{
	[Route("api/[controller]")]
	public class DapperController : BaseController
	{
		private readonly DapperRepository _repository;

		public DapperController()
		{
			_repository = new DapperRepository(ConnectionString);
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

		public async Task<IActionResult> CreateDocument(Document document)
		{
			var doc = await _repository.CreateAsync(document);
			return Ok(doc);
		}
	}
}