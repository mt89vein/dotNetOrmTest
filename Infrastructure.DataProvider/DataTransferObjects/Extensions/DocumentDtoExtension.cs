using System;
using System.Linq;
using Domain;
using Domain.Core;
using Domain.FetchStrategies;

namespace Infrastructure.DataProvider
{
	public partial class DocumentDto : IDataTransferObject<Document, DocumentWorkItemStrategy>
	{
		public Document Reconstitute(DocumentWorkItemStrategy documentWorkItemStrategy)
		{
			switch (DocumentType)
			{
				case DocumentType.OtherDocument:
					return OtherDocumentDto?.Reconstitute(new OtherDocumentWorkItemStrategy(documentWorkItemStrategy.WithDeleted));
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public void Update(Document entity, DocumentWorkItemStrategy documentWorkItemStrategy)
		{
			Id = entity.Id;
			Name = entity.Name;

			if (documentWorkItemStrategy?.WithAttachments == true)
			{
				// update 
			}
			
			if (entity is OtherDocument otherDocument && documentWorkItemStrategy is OtherDocumentWorkItemStrategy otherDocumentWorkItemStrategy)
			{
				if (OtherDocumentDto == null)
				{
					OtherDocumentDto = new OtherDocumentDto();
				}

				OtherDocumentDto.Update(otherDocument, otherDocumentWorkItemStrategy);
			}
		}
	}

	public static class DocumentDtoReconstituteExtensions
	{
		public static void UpdateFrom(this DocumentDto dto, Document entity)
		{
			dto.AttachmentDtos = entity.Attachments
				.Select(w => new AttachmentDto
			{
				Deleted = false,
				DocumentId = w.DocumentId,
				Path = w.Path
			}).ToList();

			dto.Deleted = entity.Deleted;
			dto.
		}
	}
}