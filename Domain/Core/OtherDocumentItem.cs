using Infrastructure.DomainBase;

namespace Domain
{
	public class OtherDocumentItem : Entity
	{
		public OtherDocumentItem(int id, string name, int otherDocumentId)
			: base(id)
		{
			Id = id;
			Name = name;
			OtherDocumentId = otherDocumentId;
		}

		public OtherDocumentItem(int otherDocumentId, string name)
		{
			Name = name;
			OtherDocumentId = otherDocumentId;
		}

		public string Name { get; }

		public int OtherDocumentId { get; }
	}
}