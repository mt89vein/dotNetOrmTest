using Domain;
using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider.Repositories
{
    public interface IDocumentRepository : IRepository<Document, DocumentDto, ISpecification<DocumentDto>>
    {
    }
}