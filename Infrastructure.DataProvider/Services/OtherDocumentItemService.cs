using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.FetchStrategies;
using Domain.Services;
using Infrastructure.DataProvider.Caching;
using Infrastructure.DomainBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider.Services
{
    public class OtherDocumentItemService : BaseService<OtherDocumentItem, OtherDocumentItemDto, ISpecification<OtherDocumentItemDto>>, IOtherDocumentItemService
    {
        public OtherDocumentItemService(
            IRepository<OtherDocumentItem, OtherDocumentItemDto, ISpecification<OtherDocumentItemDto>> repository,
            ApplicationContext context, ICacheService<OtherDocumentItemDto, OtherDocumentItem> cacheService) :
            base(repository, context, cacheService)
        {
        }

        public IReadOnlyCollection<OtherDocumentItem> GetByOtherDocumentId(int otherDocumentId,
            OtherDocumentItemWorkItemStrategy otherDocumentItemWorkItemStrategy)
        {
            var spec = new Specification<OtherDocumentItemDto>(w => w.OtherDocumentId == otherDocumentId).And(
                ToSpecification(otherDocumentItemWorkItemStrategy));

            return Repository.GetBySpecification(specification: spec).Select(w => w.Reconstitute()).ToList();
        }

        protected override ISpecification<OtherDocumentItemDto> ToSpecification(IWorkItemStrategy workItemStrategy)
        {
            var specification = new Specification<OtherDocumentItemDto>();

            if (!(workItemStrategy is OtherDocumentItemWorkItemStrategy strategy))
            {
                return specification;
            }

            if (strategy.WithNestedItems)
            {
                if (strategy.NestedItemWorkItemStrategy.WithOneMoreNestedItems)
                {
                    specification.FetchStrategy.Add(w => w.Include(x => x.NestedItemDtos)
                        .ThenInclude(x => x.OneMoreNestedItemDtos));
                }
                else
                {
                    specification.FetchStrategy.Add(w => w.Include(x => x.NestedItemDtos));
                }
            }

            if (!strategy.WithDeleted)
            {
                specification.And(OnlyNotDeletedSpecification);
            }
            
            return specification;
        }
    }
}