using System.Collections.Generic;
using Domain.FetchStrategies;
using Infrastructure.DomainBase;

namespace Domain
{
	public interface IOtherDocumentItemRepository : IRepository<OtherDocumentItem, OtherDocumentItemWorkItemStrategy>
	{
		IReadOnlyCollection<OtherDocumentItem> GetOtherDocumentItemsByDocumentId(int documentId,
			OtherDocumentItemWorkItemStrategy otherDocumentItemWorkItemStrategy = null);
	}
}