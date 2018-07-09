using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Integration.PublishData;
using Integration.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OrmTest.ViewModels;

namespace OrmTest.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IOtherDocumentItemRepository _otherDocumentItemRepository;
        private readonly IOtherDocumentRepository _otherDocumentRepository;
        private readonly IPublishService _publishService;
        private readonly ISecondDocumentRepository _secondDocumentRepository;

        public HomeController(
            IDocumentRepository documentRepository,
            ISecondDocumentRepository secondDocumentRepository,
            IOtherDocumentRepository otherDocumentRepository,
            IPublishService publishService,
            IOtherDocumentItemRepository otherDocumentItemRepository
        )
        {
            _documentRepository = documentRepository;
            _secondDocumentRepository = secondDocumentRepository;
            _otherDocumentRepository = otherDocumentRepository;
            _publishService = publishService;
            _otherDocumentItemRepository = otherDocumentItemRepository;
        }

        [HttpGet]
        public IActionResult GetDocuments()
        {
            var documents = _documentRepository.GetAll();
            return Ok(documents);
        }

        [HttpGet]
        public IActionResult GetOtherDocuments()
        {
            var documents = _otherDocumentRepository.GetAll();
            return Ok(documents);
        }

        [HttpGet]
        public IActionResult OtherDocumentInfo(int id)
        {
            var document = _otherDocumentRepository.Get(id);
            var items = _otherDocumentItemRepository.GetOtherDocumentItemsByDocumentId(id);
           
            var payments = document.GetOtherDocumentPayments();
            var items2 = document.GetOtherDocumentItems();

            return Ok(new
            {
                document,
                items,
                items2,
                payments
            });
        }

        [HttpGet]
        public IActionResult GetSecondDocuments()
        {
            var documents = _secondDocumentRepository.GetAll();
            return Ok(documents);
        }

        [HttpPost]
        public IActionResult CreateSecondDocument([FromBody] SecondDocumentViewModel secondDocument)
        {
            // validate etc.. then save
            var doc = _secondDocumentRepository.Save(secondDocument.GetModel());
            return Ok(doc);
        }

        [HttpPost]
        public IActionResult CreateOtherDocumentItem([FromBody] OtherDocumentItemViewModel otherDocumentItemViewModel)
        {
            // validate etc.. then save
            var doc = _otherDocumentItemRepository.Save(otherDocumentItemViewModel.GetModel());
            return Ok(doc);
        }

        [HttpPost]
        public async Task<IActionResult> Publish(int id, [FromBody] OtherDocumentPublishData publishData,
            CancellationToken token = new CancellationToken())
        {
            token.ThrowIfCancellationRequested();
            var document = _otherDocumentRepository.Get(id);
            var result = await _publishService.PublishAsync(document, publishData, token);

            return Ok(result);
        }
    }
}