namespace Infrastructure.DataProvider
{
	public partial class OtherDocumentItemDto
	{
		public string Name { get; set; }

		public int OtherDocumentId { get; set; }

		public virtual OtherDocumentDto OtherDocumentDto { get; set; }

		public bool Deleted { get; set; }

		public int Id { get; set; }
	}
}