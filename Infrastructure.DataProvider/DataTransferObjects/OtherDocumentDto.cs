using System.Collections.Generic;

namespace Infrastructure.DataProvider
{
    public partial class OtherDocumentDto
    {
        public OtherDocumentDto()
        {
            OtherDocumentItemDtos = new HashSet<OtherDocumentItemDto>();
            OtherDocumentPaymentDtos = new HashSet<OtherDocumentPaymentDto>();
        }

        public string TestName { get; set; }

        public ICollection<OtherDocumentItemDto> OtherDocumentItemDtos { get; set; }

        public ICollection<OtherDocumentPaymentDto> OtherDocumentPaymentDtos { get; set; }

        public DocumentDto DocumentDto { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }
    }
}