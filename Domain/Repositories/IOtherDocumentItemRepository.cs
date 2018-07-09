using System.Collections.Generic;
using Infrastructure.DomainBase;

namespace Domain
{
    public interface IOtherDocumentItemRepository : IRepository<OtherDocumentItem>
    {
        IReadOnlyCollection<OtherDocumentItem> GetOtherDocumentItemsByDocumentId(int documentId);
    }
}