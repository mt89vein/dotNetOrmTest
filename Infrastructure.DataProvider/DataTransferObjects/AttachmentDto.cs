namespace Infrastructure.DataProvider
{
    public partial class AttachmentDto
    {
        public string Path { get; set; }

        public DocumentDto DocumentDto { get; set; }

        public int? DocumentId { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }
    }
}