using Infrastructure.DomainBase;

namespace Domain
{
	public class Attachment : Entity
	{
		public Attachment(string path, int id, bool deleted)
			: base(id, deleted)
		{
			Path = path;
		}

		public string Path { get; }
	}
}