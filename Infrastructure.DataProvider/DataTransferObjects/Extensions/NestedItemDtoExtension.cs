using System.Collections.Generic;
using System.Linq;
using Domain.Core;

namespace Infrastructure.DataProvider
{
    public partial class NestedItemDto : IDataTransferObject<NestedItem>
    {
        public NestedItem Reconstitute()
        {
            var oneMoreNestedItems = OneMoreNestedItemDtos?.Select(w => w.Reconstitute()).ToList() ?? new List<OneMoreNestedItem>();
            return new NestedItem(NestedItemName, OtherDocumentItemId, Deleted, Id, oneMoreNestedItems);
        }

        public void Update(NestedItem entity)
        {
            NestedItemName = entity.NestedItemName;
            Deleted = entity.Deleted;
            Id = entity.Id;
            OtherDocumentItemId = entity.OtherDocumentItemId;
            OneMoreNestedItemDtos = entity.OneMoreNestedItems.Select(w => new OneMoreNestedItemDto
            {
                Id = w.Id,
                Deleted = w.Deleted,
                NestedItemId = w.NestedItemId,
                OneMoreNestedItemName = w.OneMoreNestedItemName
            }).ToList();
        }
    }
}