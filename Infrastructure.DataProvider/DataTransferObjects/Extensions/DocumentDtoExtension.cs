using System;
using System.Linq;
using Domain;
using Domain.Core;
using Domain.FetchStrategies;

namespace Infrastructure.DataProvider
{
	public partial class DocumentDto : IDataTransferObject<Document>
	{
	    public Document Reconstitute()
	    {
	        switch (DocumentType)
	        {
	            case DocumentType.OtherDocument:
	                return OtherDocumentDto?.Reconstitute();
	            default:
	                throw new ArgumentOutOfRangeException();
	        }
        }

	    public void Update(Document entity)
	    {
	        Id = entity.Id;
	        Name = entity.Name;
            // update logic
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
		}
	}
}