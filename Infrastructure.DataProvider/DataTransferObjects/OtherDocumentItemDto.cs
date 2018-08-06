using System.Collections.Generic;

namespace Infrastructure.DataProvider
{
    public partial class OtherDocumentItemDto
    {
        public OtherDocumentItemDto()
        {
            NestedItemDtos = new HashSet<NestedItemDto>();
        }

        public string Name { get; set; }

        public int OtherDocumentId { get; set; }

        public OtherDocumentDto OtherDocumentDto { get; set; }

        public ICollection<NestedItemDto> NestedItemDtos { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }
    }
}