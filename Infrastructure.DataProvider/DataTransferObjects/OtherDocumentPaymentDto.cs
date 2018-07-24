namespace Infrastructure.DataProvider
{
	public partial class OtherDocumentPaymentDto
	{
		public string Total { get; set; }

		public int OtherDocumentId { get; set; }

		public virtual OtherDocumentDto OtherDocumentDto { get; set; }

		public bool Deleted { get; set; }

		public int Id { get; set; }
	}
}