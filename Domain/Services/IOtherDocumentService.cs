using Domain.FetchStrategies;

namespace Domain.Services
{
    public interface IOtherDocumentService : IBaseService<OtherDocument, OtherDocumentWorkItemStrategy>
    {
    }
}