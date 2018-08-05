using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.FetchStrategies;
using Domain.Services;
using Infrastructure.DataProvider.Caching;
using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider.Services
{
    public class OtherDocumentItemService : BaseService<OtherDocumentItem, OtherDocumentItemDto,
        OtherDocumentItemWorkItemStrategy, ISpecification<OtherDocumentItemDto>>, IOtherDocumentItemService
    {
        public OtherDocumentItemService(
            IRepository<OtherDocumentItem, OtherDocumentItemDto, ISpecification<OtherDocumentItemDto>> repository,
            ApplicationContext context, IRedisService<OtherDocumentItemDto, OtherDocumentItem> redisService) :
            base(repository, context, redisService)
        {
        }

        public IReadOnlyCollection<OtherDocumentItem> GetByOtherDocumentId(int otherDocumentId,
            OtherDocumentItemWorkItemStrategy otherDocumentItemWorkItemStrategy)
        {
            var spec = ToSpecification(otherDocumentItemWorkItemStrategy)
                .And(new Specification<OtherDocumentItemDto>(w => w.OtherDocumentId == otherDocumentId));

            return Repository.GetBySpecification(specification: spec).Select(w => w.Reconstitute()).ToList();
        }

        protected override ISpecification<OtherDocumentItemDto> ToSpecification(
            OtherDocumentItemWorkItemStrategy otherDocumentItemWorkItemStrategy)
        {
            var specification = new Specification<OtherDocumentItemDto>();

            if (otherDocumentItemWorkItemStrategy == null)
            {
                return specification;
            }

            if (!otherDocumentItemWorkItemStrategy.WithDeleted)
            {
                specification.Predicate = OnlyNotDeletedSpecification.Predicate;
            }

            return specification;
        }
    }
}