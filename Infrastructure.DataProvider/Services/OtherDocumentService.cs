using Domain;
using Domain.FetchStrategies;
using Domain.Services;
using Infrastructure.DataProvider.Caching;
using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider.Services
{
    public class OtherDocumentService : BaseService<OtherDocument, OtherDocumentDto, OtherDocumentWorkItemStrategy,
        ISpecification<OtherDocumentDto>>, IOtherDocumentService
    {
        public OtherDocumentService(
            IRepository<OtherDocument, OtherDocumentDto, ISpecification<OtherDocumentDto>> repository,
            ApplicationContext context, IRedisService<OtherDocumentDto, OtherDocument> redisService)
            : base(repository, context, redisService)
        {
        }

        protected override ISpecification<OtherDocumentDto> ToSpecification(
            OtherDocumentWorkItemStrategy otherDocumentWorkItemStrategy)
        {
            var specification = new Specification<OtherDocumentDto>();

            if (otherDocumentWorkItemStrategy == null)
            {
                return specification;
            }

            if (otherDocumentWorkItemStrategy.WithAttachments)
            {
                specification.FetchStrategy.Include(w => w.DocumentDto.AttachmentDtos);
            }

            if (otherDocumentWorkItemStrategy.WithItems)
            {
                specification.FetchStrategy.Include(w => w.OtherDocumentItemDtos);
            }

            if (otherDocumentWorkItemStrategy.WithPayments)
            {
                specification.FetchStrategy.Include(w => w.OtherDocumentPaymentDtos);
            }

            if (!otherDocumentWorkItemStrategy.WithDeleted)
            {
                specification.Predicate = OnlyNotDeletedSpecification.Predicate;
            }

            return specification;
        }
    }
}