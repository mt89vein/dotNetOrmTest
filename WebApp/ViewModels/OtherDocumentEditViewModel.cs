using System.Collections.Generic;
using System.Linq;
using Domain;

namespace WebApp.ViewModels
{
    public class OtherDocumentEditViewModel
    {
        public OtherDocumentEditViewModel(OtherDocument otherDocument)
        {
            Id = otherDocument.Id;
            Deleted = otherDocument.Deleted;
            Name = otherDocument.Name;
            TestName = otherDocument.TestName;
            Payments = otherDocument.Payments.Select(w => new OtherDocumentPaymentEditViewModel(w)).ToList();
            Items = otherDocument.Items.Select(w => new OtherDocumentItemEditViewModel(w)).ToList();
            Attachments = otherDocument.Attachments.Select(w => new AttachmentEditViewModel(w)).ToList();
        }

        public OtherDocumentEditViewModel()
        {
            Payments = new List<OtherDocumentPaymentEditViewModel>();
            Items = new List<OtherDocumentItemEditViewModel>();
            Attachments = new List<AttachmentEditViewModel>();
            Deleted = false;
        }

        public int Id { get; set; }

        public bool Deleted { get; set; }

        public string TestName { get; set; }

        public List<OtherDocumentPaymentEditViewModel> Payments { get; set; }

        public List<OtherDocumentItemEditViewModel> Items { get; set; }

        public string Name { get; set; }

        public List<AttachmentEditViewModel> Attachments { get; set; }

        public OtherDocument GetModel()
        {
            return new OtherDocument(
                Id,
                Name,
                TestName,
                Attachments.Select(w => w.GetModel()).ToList(),
                Deleted,
                Payments.Select(w => w.GetModel()).ToList(),
                Items.Select(w => w.GetModel()).ToList()
            );
        }
    }
}