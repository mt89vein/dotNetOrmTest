using Domain;

namespace Infrastructure.DataProvider
{
    public partial class AttachmentDto : IDataTransferObject<Attachment>
    {
        public Attachment Reconstitute()
        {
            return new Attachment(Path, Id, DocumentId);
        }

        public void Update(Attachment entity)
        {
            Id = entity.Id;
            Path = entity.Path;
            DocumentId = entity.DocumentId;
        }
    }
}