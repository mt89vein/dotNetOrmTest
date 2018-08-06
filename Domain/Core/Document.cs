using System.Collections.Generic;
using Infrastructure.DomainBase;

namespace Domain
{
    public abstract class Document : Entity
    {
        protected Document(int id, string name, bool deleted, ICollection<Attachment> attachments) : base(id, deleted)
        {
            Name = name;
            Attachments = attachments;
        }

        public string Name { get; }

        public ICollection<Attachment> Attachments { get; }
    }
}