using System.Collections.Generic;
using System.Linq;
using Domain.Core;
using Infrastructure.DomainBase;

namespace Domain
{
	public class OtherDocumentItem : Entity
	{
		public OtherDocumentItem(int id, string name, int otherDocumentId, bool deleted, IEnumerable<NestedItem> nestedItems)
			: base(id, deleted)
		{
			Name = name;
			OtherDocumentId = otherDocumentId;
		    NestedItems = nestedItems.ToList();
		}

		public OtherDocumentItem(int otherDocumentId, string name)
		{
			Name = name;
			OtherDocumentId = otherDocumentId;
		}

		public string Name { get; }

		public int OtherDocumentId { get; }

        public List<NestedItem> NestedItems { get; }
	}
}