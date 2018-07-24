using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.FetchStrategies;

namespace Infrastructure.DataProvider.Repositories
{
	public class OtherDocumentPaymentRepository : LinqRepository<OtherDocumentPayment, OtherDocumentPaymentDto,
			OtherDocumentPaymentWorkItemStrategy>,
		IOtherDocumentPaymentRepository
	{
		public OtherDocumentPaymentRepository(ApplicationContext context) : base(context)
		{
		}

		public IReadOnlyCollection<OtherDocumentPayment> GetOtherDocumentPaymentsByDocumentId(int documentId,
			OtherDocumentPaymentWorkItemStrategy otherDocumentPaymentWorkItemStrategy = null)
		{
			var query = QueryAll;

			if (otherDocumentPaymentWorkItemStrategy != null)
			{
				query = BaseQuery(ToSpecification(otherDocumentPaymentWorkItemStrategy));
			}

			return query
				.Where(w => w.OtherDocumentId == documentId)
				.Select(w => w.Reconstitute(otherDocumentPaymentWorkItemStrategy))
				.ToList();
		}

		protected override Specification<OtherDocumentPaymentDto> ToSpecification(
			OtherDocumentPaymentWorkItemStrategy otherDocumentPaymentWorkItemStrategy)
		{
			var specification = new Specification<OtherDocumentPaymentDto>();

			if (!otherDocumentPaymentWorkItemStrategy.WithDeleted)
			{
				specification.Predicate = w => !w.Deleted;
			}

			return specification;
		}
	}
}