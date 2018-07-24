using Domain;
using Domain.FetchStrategies;

namespace Infrastructure.DataProvider
{
	public partial class
		OtherDocumentPaymentDto : IDataTransferObject<OtherDocumentPayment, OtherDocumentPaymentWorkItemStrategy>
	{
		public OtherDocumentPayment Reconstitute(OtherDocumentPaymentWorkItemStrategy otherDocumentPaymentWorkItemStrategy)
		{
			return new OtherDocumentPayment(Id, Total, OtherDocumentId);
		}

		public void Update(OtherDocumentPayment entity,
			OtherDocumentPaymentWorkItemStrategy otherDocumentPaymentWorkItemStrategy)
		{
			Id = entity.Id;
			Total = entity.Total;
			OtherDocumentId = entity.OtherDocumentId;
		}
	}
}