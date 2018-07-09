using System.Collections.Generic;
using Domain;

namespace OrmTest.ViewModels
{
    public class OtherDocumentViewModel
    {
        public OtherDocumentViewModel(OtherDocument document)
        {
            Id = document.Id;
            Name = document.Name;
            TestName = document.TestName;
            PublicationEvent = new PublicationEventViewModel(document.PublicationEvent);
        }

        public OtherDocumentViewModel()
        {
            PublicationEvent = new PublicationEventViewModel();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string TestName { get; set; }

        public PublicationEventViewModel PublicationEvent { get; set; }

        public IReadOnlyCollection<OtherDocumentItemViewModel> OtherDocumentItems { get; set; }

        public IReadOnlyCollection<OtherDocumentPaymentViewModel> OtherDocumentPayments { get; set; }
    }
}