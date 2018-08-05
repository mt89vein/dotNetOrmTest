using Domain;
using Domain.FetchStrategies;
using Domain.Services;
using Infrastructure.DataProvider.Caching;
using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider.Services
{
    public class DocumentService : BaseService<Document, DocumentDto, DocumentWorkItemStrategy,
        ISpecification<DocumentDto>>, IDocumentService
    {
        public DocumentService(IRepository<Document, DocumentDto, ISpecification<DocumentDto>> repository,
            ApplicationContext context, IRedisService<DocumentDto, Document> redisService) : base(repository, context,
            redisService)
        {
        }

        protected override ISpecification<DocumentDto> ToSpecification(
            DocumentWorkItemStrategy documentWorkItemStrategy)
        {
            var specification = new Specification<DocumentDto>();

            if (documentWorkItemStrategy == null)
            {
                return specification;
            }

            if (documentWorkItemStrategy.WithAttachments)
            {
                specification.FetchStrategy.Include(w => w.AttachmentDtos);
            }

            if (!documentWorkItemStrategy.WithDeleted)
            {
                specification.Predicate = w => !w.Deleted;
            }

            specification.FetchStrategy.Include(w => w.OtherDocumentDto);

            return specification;
        }
    }
}