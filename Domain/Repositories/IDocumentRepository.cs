using Domain.FetchStrategies;
using Infrastructure.DomainBase;

namespace Domain
{
	public interface IDocumentRepository : IRepository<Document, DocumentWorkItemStrategy>
	{
	}
}