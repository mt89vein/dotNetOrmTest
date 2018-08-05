using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Infrastructure.DataProvider.Repositories
{
    public class OtherDocumentItemRepository :
        LinqRepository<OtherDocumentItem, OtherDocumentItemDto, ISpecification<OtherDocumentItemDto>>,
        IOtherDocumentItemRepository
    {
        public OtherDocumentItemRepository(ApplicationContext context) : base(context)
        {
        }

        public IReadOnlyCollection<OtherDocumentItemDto> GetOtherDocumentItemsByDocumentId(int documentId,
            ISpecification<OtherDocumentItemDto> specification = null)
        {
            return BaseQuery(specification)
                .Where(w => w.OtherDocumentId == documentId)
                .ToList();
        }
    }
}