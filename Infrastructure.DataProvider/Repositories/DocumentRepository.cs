using Domain;

namespace Infrastructure.DataProvider.Repositories
{
    public class DocumentRepository : LinqRepository<Document, DocumentDto, ISpecification<DocumentDto>>,
        IDocumentRepository
    {
        public DocumentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}