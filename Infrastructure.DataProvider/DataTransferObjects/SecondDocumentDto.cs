using System;

namespace Infrastructure.DataProvider
{
    public partial class SecondDocumentDto
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string DocumentSigner { get; set; }

        public virtual DocumentDto DocumentDto { get; set; }
    }
}