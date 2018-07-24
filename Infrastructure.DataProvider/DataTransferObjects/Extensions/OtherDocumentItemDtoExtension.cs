using Domain;
using Domain.FetchStrategies;

namespace Infrastructure.DataProvider
{
	public partial class OtherDocumentItemDto : IDataTransferObject<OtherDocumentItem, OtherDocumentItemWorkItemStrategy>
	{
		public OtherDocumentItem Reconstitute(OtherDocumentItemWorkItemStrategy otherDocumentItemWorkItemStrategy)
		{
			return new OtherDocumentItem(Id, Name, OtherDocumentId);
		}

		public void Update(OtherDocumentItem entity, OtherDocumentItemWorkItemStrategy workItemStrategy)
		{
			Id = entity.Id;
			Name = entity.Name;
			OtherDocumentId = entity.OtherDocumentId;
		}
	}
}