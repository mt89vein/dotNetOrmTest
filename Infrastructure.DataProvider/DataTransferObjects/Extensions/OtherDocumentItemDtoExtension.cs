using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Core;

namespace Infrastructure.DataProvider
{
    public partial class OtherDocumentItemDto : IDataTransferObject<OtherDocumentItem>
    {
        public OtherDocumentItem Reconstitute()
        {
            var nestedItems = NestedItemDtos?.Select(w => w.Reconstitute()) ?? new List<NestedItem>();
            return new OtherDocumentItem(Id, Name, OtherDocumentId, Deleted, nestedItems);
        }

        public void Update(OtherDocumentItem entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            OtherDocumentId = entity.OtherDocumentId;
            Deleted = entity.Deleted;
            NestedItemDtos = entity.NestedItems.Select(OtherDocumentItemExtension.UpdateNestedItem).ToList();
        }
    }
}