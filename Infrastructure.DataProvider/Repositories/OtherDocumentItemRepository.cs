using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.FetchStrategies;

namespace Infrastructure.DataProvider.Repositories
{
	public class OtherDocumentItemRepository :
		LinqRepository<OtherDocumentItem, OtherDocumentItemDto, OtherDocumentItemWorkItemStrategy>,
		IOtherDocumentItemRepository
	{
		public OtherDocumentItemRepository(ApplicationContext context) : base(context)
		{
		}

		public IReadOnlyCollection<OtherDocumentItem> GetOtherDocumentItemsByDocumentId(int documentId,
			OtherDocumentItemWorkItemStrategy otherDocumentItemWorkItemStrategy = null)
		{
			var query = QueryAll;

			if (otherDocumentItemWorkItemStrategy != null)
			{
				query = BaseQuery(ToSpecification(otherDocumentItemWorkItemStrategy));
			}

			return query
				.Where(w => w.OtherDocumentId == documentId)
				.Select(w => w.Reconstitute(otherDocumentItemWorkItemStrategy))
				.ToList();
		}

		protected override Specification<OtherDocumentItemDto> ToSpecification(
			OtherDocumentItemWorkItemStrategy otherDocumentItemWorkItemStrategy)
		{
			var specification = new Specification<OtherDocumentItemDto>();

			if (!otherDocumentItemWorkItemStrategy.WithDeleted)
			{
				specification.Predicate = w => !w.Deleted;
			}

			return specification;
		}
	}
}