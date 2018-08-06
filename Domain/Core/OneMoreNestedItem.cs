using Infrastructure.DomainBase;

namespace Domain.Core
{
    public class OneMoreNestedItem : Entity
    {
        public OneMoreNestedItem(string oneMoreNestedItemName, int nestedItemId, bool deleted, int id) : base(id, deleted)
        {
            OneMoreNestedItemName = oneMoreNestedItemName;
            NestedItemId = nestedItemId;
        }

        public string OneMoreNestedItemName { get; }

        public int NestedItemId { get; }
    }
}