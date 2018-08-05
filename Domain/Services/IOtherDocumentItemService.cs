using System.Collections.Generic;
using Domain.FetchStrategies;

namespace Domain.Services
{
    public interface IOtherDocumentItemService : IBaseService<OtherDocumentItem, OtherDocumentItemWorkItemStrategy>
    {
        IReadOnlyCollection<OtherDocumentItem> GetByOtherDocumentId(int otherDocumentId, OtherDocumentItemWorkItemStrategy otherDocumentItemWorkItemStrategy);
    }
}