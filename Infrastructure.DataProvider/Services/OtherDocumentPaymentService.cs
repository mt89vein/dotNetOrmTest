using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.FetchStrategies;
using Domain.Services;
using Infrastructure.DataProvider.Caching;
using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider.Services
{
    public class OtherDocumentPaymentService : BaseService<OtherDocumentPayment, OtherDocumentPaymentDto,
        OtherDocumentPaymentWorkItemStrategy, ISpecification<OtherDocumentPaymentDto>>, IOtherDocumentPaymentService
    {
        public OtherDocumentPaymentService(
            IRepository<OtherDocumentPayment, OtherDocumentPaymentDto, ISpecification<OtherDocumentPaymentDto>>
                repository, ApplicationContext context,
            IRedisService<OtherDocumentPaymentDto, OtherDocumentPayment> redisService) :
            base(repository, context, redisService)
        {
        }

        public IReadOnlyCollection<OtherDocumentPayment> GetByOtherDocumentId(int otherDocumentId,
            OtherDocumentPaymentWorkItemStrategy otherDocumentPaymentWorkItemStrategy)
        {
            var spec = ToSpecification(otherDocumentPaymentWorkItemStrategy)
                .And(new Specification<OtherDocumentPaymentDto>(w => w.OtherDocumentId == otherDocumentId));

            return Repository.GetBySpecification(specification: spec).Select(w => w.Reconstitute()).ToList();
        }

        protected override ISpecification<OtherDocumentPaymentDto> ToSpecification(
            OtherDocumentPaymentWorkItemStrategy otherDocumentPaymentWorkItemStrategy)
        {
            var specification = new Specification<OtherDocumentPaymentDto>();

            if (otherDocumentPaymentWorkItemStrategy == null)
            {
                return specification;
            }

            if (!otherDocumentPaymentWorkItemStrategy.WithDeleted)
            {
                specification.Predicate = OnlyNotDeletedSpecification.Predicate;
            }

            return specification;
        }
    }
}