using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Infrastructure.DataProvider.Repositories
{
    public class OtherDocumentItemRepository : LinqRepository<OtherDocumentItem, OtherDocumentItemDto>,
        IOtherDocumentItemRepository
    {
        public IReadOnlyCollection<OtherDocumentItem> GetOtherDocumentItemsByDocumentId(int documentId)
        {
            return QueryAll.Where(w => w.OtherDocumentId == documentId).Select(w => w.Reconstitute()).ToList();
        }

        protected override IQueryable<OtherDocumentItemDto> QueryAll => Table;
    }
}