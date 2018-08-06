using System.Collections.Generic;
using Infrastructure.DomainBase;

namespace Domain.Core
{
    public class NestedItem : Entity
    {
        public NestedItem(string nestedItemName, int otherDocumentItemId, bool deleted, int id, List<OneMoreNestedItem> oneMoreNestedItems) : base(id, deleted)
        {
            NestedItemName = nestedItemName;
            OtherDocumentItemId = otherDocumentItemId;
            OneMoreNestedItems = oneMoreNestedItems;
        }

        public string NestedItemName { get; }

        public int OtherDocumentItemId { get; }

        public List<OneMoreNestedItem> OneMoreNestedItems { get; }
    }
}