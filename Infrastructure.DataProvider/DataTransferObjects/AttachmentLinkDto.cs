namespace Infrastructure.DataProvider
{
    public class AttachmentLinkDto
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }

        public DocumentDto DocumentDto { get; set; }

        public int AttachmentId { get; set; }

        public AttachmentDto AttachmentDto { get; set; }
    }
}