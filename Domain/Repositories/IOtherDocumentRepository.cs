using Domain.FetchStrategies;
using Infrastructure.DomainBase;

namespace Domain
{
	public interface IOtherDocumentRepository : IRepository<OtherDocument, OtherDocumentWorkItemStrategy>
	{
	}
}