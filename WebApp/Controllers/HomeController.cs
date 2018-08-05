using Domain.FetchStrategies;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IActionResult OtherDocumentInfo(
            int id,
            bool attachments = true,
            bool payments = true,
            bool items = true,
            bool readOnly = true,
            bool cacheResult = true,
            bool deleted = false
        )
        {
            var otherDocumentItemWorkItemStrategy = new OtherDocumentItemWorkItemStrategy(deleted, true, true);
            var otherDocumentPaymentWorkItemStrategy = new OtherDocumentPaymentWorkItemStrategy(deleted, true, true);
            var strategy = new OtherDocumentWorkItemStrategy(deleted, attachments, payments, items, readOnly,
                cacheResult, otherDocumentItemWorkItemStrategy, otherDocumentPaymentWorkItemStrategy);

            var document = _otherDocumentService.Get(id, strategy);
            var documentItems = _otherDocumentItemService.GetByOtherDocumentId(id, otherDocumentItemWorkItemStrategy);
            var documentPayments =  _otherDocumentPaymentService.GetByOtherDocumentId(id, otherDocumentPaymentWorkItemStrategy);

            return Ok(new
            {
                document,
                documentItems,
                documentPayments
            });
        }
    }
}