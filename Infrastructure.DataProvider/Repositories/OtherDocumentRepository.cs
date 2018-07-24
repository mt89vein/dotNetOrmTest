using Domain;
using Domain.FetchStrategies;

namespace Infrastructure.DataProvider.Repositories
{
	public class OtherDocumentRepository : LinqRepository<OtherDocument, OtherDocumentDto, OtherDocumentWorkItemStrategy>,
		IOtherDocumentRepository
	{
		public OtherDocumentRepository(ApplicationContext context)
			: base(context)
		{
		}

		protected sealed override Specification<OtherDocumentDto> ToSpecification(OtherDocumentWorkItemStrategy otherDocumentWorkItemStrategy)
		{
			var specification = new Specification<OtherDocumentDto>();

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
				specification.Predicate = w => !w.Deleted;
			}

			return specification;
		}
	}
}