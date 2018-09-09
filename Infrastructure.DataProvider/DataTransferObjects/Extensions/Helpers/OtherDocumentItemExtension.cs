using System.Linq;
using Domain;
using Domain.Core;

namespace Infrastructure.DataProvider
{
    public static class OtherDocumentItemExtension
    {
        public static NestedItemDto UpdateNestedItem(this NestedItem nestedItem)
        {
            return new NestedItemDto
            {
                Id = nestedItem.Id,
                Deleted = nestedItem.Deleted,
                NestedItemName = nestedItem.NestedItemName,
                OtherDocumentItemId = nestedItem.OtherDocumentItemId,
                OneMoreNestedItemDtos = nestedItem.OneMoreNestedItems.Select(UpdateOneMoreNestedItem).ToList()
            };
        }

        public static OneMoreNestedItemDto UpdateOneMoreNestedItem(this OneMoreNestedItem oneMoreNestedItem)
        {
            return new OneMoreNestedItemDto
            {
                Id = oneMoreNestedItem.Id,
                Deleted = oneMoreNestedItem.Deleted,
                NestedItemId = oneMoreNestedItem.NestedItemId,
                OneMoreNestedItemName = oneMoreNestedItem.OneMoreNestedItemName
            };
        }

        public static OtherDocumentItemDto UpdateItem(this OtherDocumentItem item)
        {
            return new OtherDocumentItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Deleted = item.Deleted,
                OtherDocumentId = item.OtherDocumentId,
                NestedItemDtos = item.NestedItems.Select(UpdateNestedItem).ToList()
            };
        }
    }
}