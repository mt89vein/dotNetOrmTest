using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Core;
using Domain.FetchStrategies;

namespace Infrastructure.DataProvider
{
	public partial class OtherDocumentDto : IDataTransferObject<OtherDocument, OtherDocumentWorkItemStrategy>
	{
		public OtherDocument Reconstitute(OtherDocumentWorkItemStrategy workItemStrategy)
		{
			var attachments = workItemStrategy?.WithAttachments == true
				? DocumentDto.AttachmentDtos.Select(w => w.Reconstitute(null)).ToList()
				: new List<Attachment>();

			return new OtherDocument(Id, DocumentDto.Name, TestName, attachments, Deleted);
		}

		public void Update(OtherDocument entity, OtherDocumentWorkItemStrategy otherDocumentWorkItemStrategy)
		{
			if (DocumentDto == null)
			{
				DocumentDto = new DocumentDto
				{
					Id = Id
				};
			}

			DocumentDto.Update(entity, otherDocumentWorkItemStrategy);
			DocumentDto.DocumentType = DocumentType.OtherDocument;
			TestName = entity.TestName;
		}
	}
}