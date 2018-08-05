using System.Collections.Generic;
using Infrastructure.DomainBase;

namespace Domain
{
	public abstract class Document : Entity
	{
		protected Document(int id, string name, List<Attachment> attachments, bool deleted)
		{
			Id = id;
			Name = name;
			Attachments = attachments;
			Deleted = deleted;
		}

		public string Name { get; }

		public List<Attachment> Attachments { get; }

		public bool Deleted { get; }
	}
}