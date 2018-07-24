using Domain;
using Domain.FetchStrategies;
using Microsoft.AspNetCore.Mvc;

namespace OrmTest.Controllers
{
	[Route("api/[controller]/[action]")]
	public class HomeController : Controller
	{
		private readonly IDocumentRepository _documentRepository;
		private readonly IOtherDocumentItemRepository _otherDocumentItemRepository;
		private readonly IOtherDocumentPaymentRepository _otherDocumentPaymentRepository;
		private readonly IOtherDocumentRepository _otherDocumentRepository;

		public HomeController(
			IDocumentRepository documentRepository,
			IOtherDocumentRepository otherDocumentRepository,
			IOtherDocumentItemRepository otherDocumentItemRepository,
			IOtherDocumentPaymentRepository otherDocumentPaymentRepository
		)
		{
			_documentRepository = documentRepository;
			_otherDocumentRepository = otherDocumentRepository;
			_otherDocumentItemRepository = otherDocumentItemRepository;
			_otherDocumentPaymentRepository = otherDocumentPaymentRepository;
		}

		[HttpGet]
		public IActionResult GetDocuments()
		{
			var documents = _documentRepository.GetAll();
			return Ok(documents);
		}

		[HttpGet]
		public IActionResult GetOtherDocuments(bool attachments = true, bool payments = true, bool items = true, bool deleted = false)
		{
			var strategy = new OtherDocumentWorkItemStrategy(deleted, attachments, payments, items);
			var documents = _otherDocumentRepository.GetAll(strategy);
			return Ok(documents);
		}

		[HttpGet]
		public IActionResult OtherDocumentInfo(int id, bool attachments = true, bool payments = true, bool items = true, bool deleted = false)
		{
			var strategy = new OtherDocumentWorkItemStrategy(deleted, attachments, payments, items);
			var document = _otherDocumentRepository.Get(id, strategy);

			var documentItems = _otherDocumentItemRepository.GetOtherDocumentItemsByDocumentId(id,
				new OtherDocumentItemWorkItemStrategy(deleted));

			var documentPayments = _otherDocumentPaymentRepository.GetOtherDocumentPaymentsByDocumentId(id,
				new OtherDocumentPaymentWorkItemStrategy(deleted));

			return Ok(new
			{
				document,
				documentItems,
				documentPayments
			});
		}
	}
}