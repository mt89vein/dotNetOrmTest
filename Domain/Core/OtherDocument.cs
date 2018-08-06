using System.Collections.Generic;

namespace Domain
{
    public class OtherDocument : Document
    {
        public OtherDocument(int id, string name, string testName, ICollection<Attachment> attachments, bool deleted,
            List<OtherDocumentPayment> payments, List<OtherDocumentItem> items)
            : base(id, name, deleted, attachments)
        {
            TestName = testName;
            Payments = payments;
            Items = items;
        }

        public string TestName { get; }

        public List<OtherDocumentPayment> Payments { get; }

        public List<OtherDocumentItem> Items { get; }
    }
}