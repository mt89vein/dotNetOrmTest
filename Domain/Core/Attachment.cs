using Infrastructure.DomainBase;

namespace Domain
{
	public class Attachment : Entity
	{
		public Attachment(string path, int id, int? documentId)
			: base(id)
		{
			Path = path;
			DocumentId = documentId;
		}

		public string Path { get; }

		public int? DocumentId { get; }
	}
}