using System.Collections.Generic;

namespace Infrastructure.DataProvider
{
    public partial class NestedItemDto
    {
        public NestedItemDto()
        {
            OneMoreNestedItemDtos = new HashSet<OneMoreNestedItemDto>();
        }

        public string NestedItemName { get; set; }

        public int OtherDocumentItemId { get; set; }

        public OtherDocumentItemDto OtherDocumentItemDto { get; set; }

        public ICollection<OneMoreNestedItemDto> OneMoreNestedItemDtos { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }
    }
}