using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.FetchStrategies;
using Domain.Services;
using Infrastructure.DataProvider.Caching;
using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider.Services
{
    public class OtherDocumentPaymentService : BaseService<OtherDocumentPayment, OtherDocumentPaymentDto, ISpecification<OtherDocumentPaymentDto>>, IOtherDocumentPaymentService
    {
        public OtherDocumentPaymentService(
            IRepository<OtherDocumentPayment, OtherDocumentPaymentDto, ISpecification<OtherDocumentPaymentDto>>
                repository, ApplicationContext context,
            ICacheService<OtherDocumentPaymentDto, OtherDocumentPayment> cacheService) :
            base(repository, context, cacheService)
        {
        }

        public IReadOnlyCollection<OtherDocumentPayment> GetByOtherDocumentId(int otherDocumentId,
            OtherDocumentPaymentWorkItemStrategy otherDocumentPaymentWorkItemStrategy)
        {
            var spec = ToSpecification(otherDocumentPaymentWorkItemStrategy)
                .And(new Specification<OtherDocumentPaymentDto>(w => w.OtherDocumentId == otherDocumentId));

            return Repository.GetBySpecification(specification: spec).Select(w => w.Reconstitute()).ToList();
        }

        protected override ISpecification<OtherDocumentPaymentDto> ToSpecification(IWorkItemStrategy workItemStrategy)
        {
            var specification = new Specification<OtherDocumentPaymentDto>();

            if (!(workItemStrategy is OtherDocumentPaymentWorkItemStrategy strategy))
            {
                return specification;
            }

            if (!strategy.WithDeleted)
            {
                specification.And(OnlyNotDeletedSpecification);
            }

            return specification;
        }
    }
}