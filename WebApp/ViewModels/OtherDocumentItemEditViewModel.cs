using System.Collections.Generic;
using System.Linq;
using Domain;

namespace WebApp.ViewModels
{
    public class OtherDocumentItemEditViewModel
    {
        public OtherDocumentItemEditViewModel(OtherDocumentItem item)
        {
            Id = item.Id;
            Deleted = item.Deleted;
            Name = item.Name;
            OtherDocumentId = item.OtherDocumentId;
            NestedItems = item.NestedItems.Select(w => new NestedItemEditViewModel(w)).ToList();
        }

        public OtherDocumentItemEditViewModel()
        {
            Deleted = false;
            NestedItems = new List<NestedItemEditViewModel>();
        }

        public int Id { get; set; }

        public bool Deleted { get; set; }

        public string Name { get; set; }

        public int OtherDocumentId { get; set; }

        public List<NestedItemEditViewModel> NestedItems { get; set; }

        public OtherDocumentItem GetModel()
        {
            return new OtherDocumentItem(Id, Name, OtherDocumentId, Deleted, NestedItems.Select(w => w.GetModel()));
        }
    }
}