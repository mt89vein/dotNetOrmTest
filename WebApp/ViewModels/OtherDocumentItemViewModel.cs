using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace OrmTest.ViewModels
{
    public class OtherDocumentItemViewModel
    {

        public OtherDocumentItemViewModel()
        {
        }

        public OtherDocumentItemViewModel(OtherDocumentItem payment)
        {
            Id = payment.Id;
            Name = payment.Name;
            OtherDocumentId = payment.OtherDocumentId;
        }

        public int? Id { get; set; }

        public int OtherDocumentId { get; set; }
        public string Name { get; set; }

        public OtherDocumentItem GetModel()
        {
            return Id.HasValue
                ? new OtherDocumentItem(Id.Value, Name, OtherDocumentId)
                : new OtherDocumentItem(OtherDocumentId, Name);
        }
    }
}
