using System.Collections.Generic;
using Domain.Core;

namespace Infrastructure.DataProvider
{
    public partial class DocumentDto
    {
        public string Name { get; set; }

        public DocumentType DocumentType { get; set; }

        public virtual OtherDocumentDto OtherDocumentDto { get; set; }

        public virtual ICollection<AttachmentDto> AttachmentDtos { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }
    }
}