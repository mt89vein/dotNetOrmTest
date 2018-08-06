using Domain;
using Domain.FetchStrategies;
using Domain.Services;
using Infrastructure.DataProvider.Caching;
using Infrastructure.DomainBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider.Services
{
    public class OtherDocumentService : BaseService<OtherDocument, OtherDocumentDto, ISpecification<OtherDocumentDto>>, IOtherDocumentService
    {
        public OtherDocumentService(
            IRepository<OtherDocument, OtherDocumentDto, ISpecification<OtherDocumentDto>> repository,
            ApplicationContext context, IRedisService<OtherDocumentDto, OtherDocument> redisService)
            : base(repository, context, redisService)
        {
        }

        protected override ISpecification<OtherDocumentDto> ToSpecification(IWorkItemStrategy workItemStrategy)
        {
            var specification = new Specification<OtherDocumentDto>();

            if (!(workItemStrategy is OtherDocumentWorkItemStrategy strategy))
            {
                return specification;
            }

            if (strategy.WithAttachments)
            {
                specification.FetchStrategy.Add(
                    w => w.Include(x => x.DocumentDto)
                            .ThenInclude(x => x.AttachmentLinkDtos)
                                .ThenInclude(x => x.AttachmentDto)
                );
            }

            if (strategy.WithItems)
            {
                if (strategy.OtherDocumentItemWorkItemStrategy.WithNestedItems)
                {
                    if (strategy.OtherDocumentItemWorkItemStrategy.NestedItemWorkItemStrategy
                        .WithOneMoreNestedItems)
                    {
                        specification.FetchStrategy.Add(
                            w => w.Include(x => x.OtherDocumentItemDtos)
                                        .ThenInclude(x => x.NestedItemDtos)
                                            .ThenInclude(x => x.OneMoreNestedItemDtos)
                        );
                    }
                    else
                    {
                        specification.FetchStrategy.Add(
                            w => w.Include(x => x.OtherDocumentItemDtos)
                                      .ThenInclude(x => x.NestedItemDtos));
                    }
                }
                else
                {
                    specification.FetchStrategy.Add(w => w.Include(x => x.OtherDocumentItemDtos));
                }
            }

            if (strategy.WithPayments)
            {
                specification.FetchStrategy.Add(w => w.Include(x => x.OtherDocumentPaymentDtos));
            }

            if (!strategy.WithDeleted)
            {
                specification.And(OnlyNotDeletedSpecification);
            }

            return specification;
        }
    }
}