using System.Collections.Generic;
using Domain.Core;

namespace Domain
{
    public class OtherDocument : Document, IPublishable
    {
        public new static IOtherDocumentRepository Repository => ObjectFactory.Instance.GetObject<IOtherDocumentRepository>();
      //  public new static IOtherDocumentRepository Repository;

       // public static IOtherDocumentItemRepository OtherDocumentItemRepository;
       // public static IOtherDocumentPaymentRepository OtherDocumentPaymentRepository;

        public OtherDocument(int id, PublicationEvent publicationEvent, string name, string testName)
            : base(id, name)
        {
            TestName = testName;
            PublicationEvent = publicationEvent;
            State = PublishState.Dispatching;
            PublicationEvent = publicationEvent;
        }

        //protected OtherDocument(
        //    IOtherDocumentRepository otherDocumentRepository,
        //    IOtherDocumentItemRepository otherDocumentItemRepository,
        //    IOtherDocumentPaymentRepository otherDocumentPaymentRepository,
        //    IDocumentRepository documentRepository
        //)
        //    : base(documentRepository)
        //{
        // /   Repository = otherDocumentRepository;
        //    OtherDocumentItemRepository = otherDocumentItemRepository;
        //    OtherDocumentPaymentRepository = otherDocumentPaymentRepository;
        //}

        public string TestName { get; }

        public PublishState? State { get; }

        public PublicationEvent PublicationEvent { get; }

        public IReadOnlyCollection<OtherDocumentItem> GetOtherDocumentItems()
        {
            return OtherDocumentItem.Repository.GetOtherDocumentItemsByDocumentId(Id);
        }

        public IReadOnlyCollection<OtherDocumentPayment> GetOtherDocumentPayments()
        {
            return OtherDocumentPayment.Repository.GetOtherDocumentPaymentsByDocumentId(Id);
        }
    }
}