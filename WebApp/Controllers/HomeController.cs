using Domain.FetchStrategies;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly IOtherDocumentItemService _otherDocumentItemService;
        private readonly IOtherDocumentPaymentService _otherDocumentPaymentService;
        private readonly IOtherDocumentService _otherDocumentService;

        public HomeController(
            IDocumentService documentService,
            IOtherDocumentItemService otherDocumentItemService,
            IOtherDocumentPaymentService otherDocumentPaymentService,
            IOtherDocumentService otherDocumentService
        )
        {
            _documentService = documentService;
            _otherDocumentItemService = otherDocumentItemService;
            _otherDocumentPaymentService = otherDocumentPaymentService;
            _otherDocumentService = otherDocumentService;
        }

        [HttpGet]
        public IActionResult GetDocuments()
        {
            var documents = _documentService.Get();
            return Ok(documents);
        }

        [HttpGet]
        public IActionResult GetOtherDocuments(bool attachments = true, bool payments = true, bool items = true,
            bool deleted = false)
        {
            var strategy = new OtherDocumentWorkItemStrategy(deleted, attachments, payments, items);
            var documents = _otherDocumentService.Get(strategy);
            return Ok(documents);
        }

        [HttpPost]
        public IActionResult UpdateOtherDocument(int id, OtherDocumentEditViewModel viewModel)
        {
            _otherDocumentService.Update(viewModel.GetModel());

            return Ok();
        }

        [HttpPost]
        public IActionResult InsertOtherDocument(int id, OtherDocumentEditViewModel viewModel)
        {
            _otherDocumentService.Insert(viewModel.GetModel());

            return Ok();
        }

        [HttpGet]
        public IActionResult OtherDocumentInfo(
            int id,
            bool attachments = true,
            bool payments = true,
            bool items = true,
            bool readOnly = true,
            bool cacheResult = true,
            bool deleted = false,
            bool withNestedItems = true,
            bool withOneMoreNestedItems = true
        )
        {
            var nestedItemWorkItemStrategy =
                new NestedItemWorkItemStrategy(withOneMoreNestedItems, deleted, readOnly, cacheResult);
            var otherDocumentItemWorkItemStrategy = new OtherDocumentItemWorkItemStrategy(withNestedItems,
                nestedItemWorkItemStrategy, deleted, readOnly, cacheResult);
            var otherDocumentPaymentWorkItemStrategy =
                new OtherDocumentPaymentWorkItemStrategy(deleted, readOnly, cacheResult);
            var strategy = new OtherDocumentWorkItemStrategy(deleted, attachments, payments, items, readOnly,
                cacheResult, otherDocumentItemWorkItemStrategy, otherDocumentPaymentWorkItemStrategy);

            var document = _otherDocumentService.Get(id, strategy);

            return Ok(document);
        }
    }
}