namespace Infrastructure.DataProvider
{
    public partial class OtherDocumentPaymentDto
    {
        public int Id { get; set; }

        public string Total { get; set; }

        public int OtherDocumentId { get; set; }

        public virtual OtherDocumentDto OtherDocumentDto { get; set; }
    }
}