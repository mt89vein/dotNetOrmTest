using Domain.FetchStrategies;

namespace Domain.Services
{
    public interface IDocumentService : IBaseService<Document, DocumentWorkItemStrategy>
    {
    }
}