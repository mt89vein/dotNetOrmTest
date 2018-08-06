using System.Collections.Generic;
using Domain.FetchStrategies;

namespace Domain.Services
{
    public interface IOtherDocumentItemService : IBaseService<OtherDocumentItem>
    {
        IReadOnlyCollection<OtherDocumentItem> GetByOtherDocumentId(int otherDocumentId, OtherDocumentItemWorkItemStrategy otherDocumentItemWorkItemStrategy);
    }
}