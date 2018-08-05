using System.Collections.Generic;

namespace Infrastructure.DataProvider
{
    public partial class OtherDocumentDto
    {
        public string TestName { get; set; }

        public virtual ICollection<OtherDocumentItemDto> OtherDocumentItemDtos { get; set; }

        public virtual ICollection<OtherDocumentPaymentDto> OtherDocumentPaymentDtos { get; set; }

        public virtual DocumentDto DocumentDto { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }
    }
}