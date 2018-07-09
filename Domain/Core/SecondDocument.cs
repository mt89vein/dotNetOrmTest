using System;

namespace Domain
{
    public class SecondDocument : Document
    {
        public SecondDocument(int id, string name, string documentSigner,
            DateTime createdAt)
            : base(id, name)
        {
            DocumentSigner = documentSigner;
            CreatedAt = createdAt;
        }

        public DateTime CreatedAt { get; set; }

        public string DocumentSigner { get; set; }
    }
}