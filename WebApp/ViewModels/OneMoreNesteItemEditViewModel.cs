using Domain.Core;

namespace WebApp.ViewModels
{
    public class OneMoreNesteItemEditViewModel
    {
        public OneMoreNesteItemEditViewModel(OneMoreNestedItem oneMoreNestedItem)
        {
            Id = oneMoreNestedItem.Id;
            Deleted = oneMoreNestedItem.Deleted;
            OneMoreNestedItemName = oneMoreNestedItem.OneMoreNestedItemName;
            NestedItemId = oneMoreNestedItem.NestedItemId;
        }

        public OneMoreNesteItemEditViewModel()
        {
            Deleted = false;
        }

        public int Id { get; set; }

        public bool Deleted { get; set; }

        public string OneMoreNestedItemName { get; set; }

        public int NestedItemId { get; set; }

        public OneMoreNestedItem GetModel()
        {
            return new OneMoreNestedItem(OneMoreNestedItemName, NestedItemId, Deleted, Id);
        }
    }
}