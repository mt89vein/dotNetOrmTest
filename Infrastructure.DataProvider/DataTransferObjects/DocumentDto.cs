using System.Collections.Generic;
using Domain.Core;

namespace Infrastructure.DataProvider
{
    public partial class DocumentDto
    {
        public DocumentDto()
        {
            AttachmentLinkDtos = new HashSet<AttachmentLinkDto>();
        }

        public string Name { get; set; }

        public DocumentType DocumentType { get; set; }

        public OtherDocumentDto OtherDocumentDto { get; set; }

        public ICollection<AttachmentLinkDto> AttachmentLinkDtos { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }
    }
}