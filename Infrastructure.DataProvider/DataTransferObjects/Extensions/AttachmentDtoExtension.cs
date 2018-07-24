using Domain;
using Domain.FetchStrategies;

namespace Infrastructure.DataProvider
{
	public partial class AttachmentDto : IDataTransferObject<Attachment, AttachmentWorkItemStrategy>
	{
		public Attachment Reconstitute(AttachmentWorkItemStrategy attachmentWorkItemStrategy)
		{
			return new Attachment(Path, Id, DocumentId);
		}

		public void Update(Attachment entity, AttachmentWorkItemStrategy attachmentWorkItemStrategy)
		{
			Id = entity.Id;
			Path = entity.Path;
			DocumentId = entity.DocumentId;
		}
	}
}