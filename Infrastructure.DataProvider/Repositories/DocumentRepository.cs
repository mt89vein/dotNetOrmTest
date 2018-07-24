using System;
using Domain;
using Domain.FetchStrategies;

namespace Infrastructure.DataProvider.Repositories
{
	public class DocumentRepository : LinqRepository<Document, DocumentDto, DocumentWorkItemStrategy>, IDocumentRepository
	{
		private readonly IOtherDocumentRepository _otherDocumentRepository;

		public DocumentRepository(IOtherDocumentRepository otherDocumentRepository, ApplicationContext context)
			: base(context)
		{
			_otherDocumentRepository = otherDocumentRepository;
		}

		public override bool Save(Document entity, DocumentWorkItemStrategy documentWorkItemStrategy = null)
		{
			if (entity is OtherDocument otherDocument &&
			    documentWorkItemStrategy is OtherDocumentWorkItemStrategy otherDocumentWorkItemStrategy)
			{
				return _otherDocumentRepository.Save(otherDocument, otherDocumentWorkItemStrategy);
			}

			throw new Exception("Неизвестный тип документа");
		}

		protected sealed override Specification<DocumentDto> ToSpecification(DocumentWorkItemStrategy workItemStrategy)
		{
			var specification = new Specification<DocumentDto>();

			if (workItemStrategy.WithAttachments)
			{
				specification.FetchStrategy.Include(w => w.AttachmentDtos);
			}

			if (!workItemStrategy.WithDeleted)
			{
				specification.Predicate = w => !w.Deleted;
			}

			specification.FetchStrategy.Include(w => w.OtherDocumentDto);

			return specification;
		}
	}
}