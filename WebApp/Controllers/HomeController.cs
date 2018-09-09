using System.Collections.Generic;
using System.Linq;
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
        public IActionResult GetDocument(int id)
        {
            var document = _documentService.Get(new DocumentWorkItemStrategy(withAttachments: false));
            return Ok(document);
        }

        [HttpGet]
        public IActionResult GetDocuments()
        {
            var documents = _documentService.Get(new DocumentWorkItemStrategy(withAttachments: false));
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

        private static void MakeUserEdit(ref OtherDocumentEditViewModel otherDocument)
        {
            otherDocument.Name = "New Document Name";
            foreach (var payment in otherDocument.Payments)
            {
                payment.Total = "500$";
            }
            foreach (var item in otherDocument.Items)
            {
                item.Name = "new item name, oldName:" + item.Name;

                var itemToDelete = item.NestedItems.FirstOrDefault(w => w.Id != default(int));
                item.NestedItems.Remove(itemToDelete);
            }
            otherDocument.Items.Add(new OtherDocumentItemEditViewModel
            {
                Name = "new item created in make user edit",
                NestedItems = new List<NestedItemEditViewModel>
                {
                    new NestedItemEditViewModel
                    {
                        NestedItemName = "new nested item name 1",
                        OneMoreNestedItems = new List<OneMoreNesteItemEditViewModel>
                        {
                            new OneMoreNesteItemEditViewModel
                            {
                                OneMoreNestedItemName = "one more nested item name 1"
                            },
                            new OneMoreNesteItemEditViewModel
                            {
                                OneMoreNestedItemName = "ss 2"
                            },
                        },
                    },
                    new NestedItemEditViewModel
                    {
                        NestedItemName = "new nested item name 2",
                        OneMoreNestedItems = new List<OneMoreNesteItemEditViewModel>
                        {
                            new OneMoreNesteItemEditViewModel
                            {
                                OneMoreNestedItemName = "one more nested item name 3"
                            },
                            new OneMoreNesteItemEditViewModel
                            {
                                OneMoreNestedItemName = "ss 4"
                            },
                        },
                    }
                }
            });
            otherDocument.Payments.Add(new OtherDocumentPaymentEditViewModel
            {
                Total = "100500 in make user edit"
            });
        }

        [HttpGet]
        public IActionResult UpdateOtherDocument(int id)
        {
            var strategy = new OtherDocumentWorkItemStrategy(withDeleted:false, withAttachments: true, withPayments: false, withItems: true, cacheResult:false,
                otherDocumentItemWorkItemStrategy: new OtherDocumentItemWorkItemStrategy(true, new NestedItemWorkItemStrategy()));

            var otherDocument = _otherDocumentService.Get(id, strategy);
            var viewModel = new OtherDocumentEditViewModel(otherDocument);

            MakeUserEdit(ref viewModel);

            _otherDocumentService.Save(viewModel.GetModel(), strategy);

            return Ok();
        }

        [HttpPost]
        public IActionResult UpdateOtherDocument(int id, OtherDocumentEditViewModel viewModel)
        {
            _otherDocumentService.Save(viewModel.GetModel() /* 
                Сюда нужно передать стратегию обновления, так как мы не знаем как именно данные запрашивались,
                судя по всему он будет храниться во viewModel и отдаваться клиенту и возвращаться, либо формировать основываясь на бизнес логике */);
            var updatedOtherDocument = _otherDocumentService.Get(id);
            return Ok(updatedOtherDocument);
        }

        [HttpPost]
        public IActionResult InsertOtherDocument(int id, OtherDocumentEditViewModel viewModel)
        {
            _otherDocumentService.Save(viewModel.GetModel());

            return Ok();
        }

        [HttpGet]
        public IActionResult OtherDocumentInfo(
            int id,
            bool attachments = true,
            bool payments = true,
            bool items = true,
            bool cacheResult = true,
            bool deleted = false,
            bool withNestedItems = true,
            bool withOneMoreNestedItems = true
        )
        {
            var nestedItemWorkItemStrategy =
                new NestedItemWorkItemStrategy(withOneMoreNestedItems, deleted, cacheResult);
            var otherDocumentItemWorkItemStrategy =
                new OtherDocumentItemWorkItemStrategy(withNestedItems, nestedItemWorkItemStrategy, deleted,
                    cacheResult);
            var otherDocumentPaymentWorkItemStrategy = new OtherDocumentPaymentWorkItemStrategy(deleted, cacheResult);
            var strategy = new OtherDocumentWorkItemStrategy(deleted, attachments, payments, items, cacheResult,
                otherDocumentItemWorkItemStrategy, otherDocumentPaymentWorkItemStrategy);

            var document = _otherDocumentService.Get(id, strategy);

            return Ok(document);
        }
    }
}