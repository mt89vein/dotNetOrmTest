using Domain;
using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider.Repositories
{
    public interface IOtherDocumentRepository : IRepository<OtherDocument, OtherDocumentDto,
        ISpecification<OtherDocumentDto>>
    {
    }
}