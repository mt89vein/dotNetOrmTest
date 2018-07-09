using Infrastructure.DomainBase;

namespace Domain
{
    public abstract class Document : Entity
    {
        public static IDocumentRepository Repository => ObjectFactory.Instance.GetObject<IDocumentRepository>();

        protected Document(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Name { get; }
    }
}