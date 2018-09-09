using System.Collections.Generic;

namespace Infrastructure.DataProvider
{
    public partial class NestedItemDto
    {
        public NestedItemDto()
        {
            OneMoreNestedItemDtos = new List<OneMoreNestedItemDto>();
        }

        public string NestedItemName { get; set; }

        public int OtherDocumentItemId { get; set; }

        public OtherDocumentItemDto OtherDocumentItemDto { get; set; }

        public List<OneMoreNestedItemDto> OneMoreNestedItemDtos { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }
    }
}