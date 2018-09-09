using System.Collections.Generic;

namespace Infrastructure.DataProvider
{
    public partial class OtherDocumentDto
    {
        public OtherDocumentDto()
        {
            OtherDocumentItemDtos = new List<OtherDocumentItemDto>();
            OtherDocumentPaymentDtos = new List<OtherDocumentPaymentDto>();
        }

        public string TestName { get; set; }

        public List<OtherDocumentItemDto> OtherDocumentItemDtos { get; set; }

        public List<OtherDocumentPaymentDto> OtherDocumentPaymentDtos { get; set; }

        public DocumentDto DocumentDto { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }
    }
}