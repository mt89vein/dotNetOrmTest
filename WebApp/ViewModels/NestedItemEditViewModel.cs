using System.Collections.Generic;
using System.Linq;
using Domain.Core;

namespace WebApp.ViewModels
{
    public class NestedItemEditViewModel
    {
        public NestedItemEditViewModel()
        {
            Deleted = false;
            OneMoreNestedItems = new List<OneMoreNesteItemEditViewModel>();
        }

        public NestedItemEditViewModel(NestedItem nestedItem)
        {
            Id = nestedItem.Id;
            Deleted = nestedItem.Deleted;
            NestedItemName = nestedItem.NestedItemName;
            OtherDocumentItemId = nestedItem.OtherDocumentItemId;
            OneMoreNestedItems = nestedItem.OneMoreNestedItems.Select(w => new OneMoreNesteItemEditViewModel(w))
                .ToList();
        }

        public int Id { get; set; }

        public bool Deleted { get; set; }

        public string NestedItemName { get; set; }

        public int OtherDocumentItemId { get; set; }

        public List<OneMoreNesteItemEditViewModel> OneMoreNestedItems { get; set; }

        public NestedItem GetModel()
        {
            return new NestedItem(
                NestedItemName,
                OtherDocumentItemId,
                Deleted,
                Id,
                Enumerable.Select<OneMoreNesteItemEditViewModel, OneMoreNestedItem>(OneMoreNestedItems, w => w.GetModel()).ToList()
            );
        }
    }
}