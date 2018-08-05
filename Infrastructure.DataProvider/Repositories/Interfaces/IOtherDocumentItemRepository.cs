using System.Collections.Generic;
using Domain;
using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider.Repositories
{
    public interface IOtherDocumentItemRepository : IRepository<OtherDocumentItem, OtherDocumentItemDto,
        ISpecification<OtherDocumentItemDto>>
    {
        IReadOnlyCollection<OtherDocumentItemDto> GetOtherDocumentItemsByDocumentId(int documentId,
            ISpecification<OtherDocumentItemDto> specification);
    }
}