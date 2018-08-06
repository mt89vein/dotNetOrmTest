using Domain.Core;

namespace Infrastructure.DataProvider
{
    public partial class OneMoreNestedItemDto : IDataTransferObject<OneMoreNestedItem>
    {
        public OneMoreNestedItem Reconstitute()
        {
            return new OneMoreNestedItem(OneMoreNestedItemName, NestedItemId, Deleted, Id);
        }

        public void Update(OneMoreNestedItem entity)
        {
            OneMoreNestedItemName = entity.OneMoreNestedItemName;
            Deleted = entity.Deleted;
            Id = entity.Id;
            NestedItemId = entity.NestedItemId;
        }
    }
}