using Domain;

namespace Infrastructure.DataProvider
{
    public partial class OtherDocumentItemDto : IDataTransferObject<OtherDocumentItem>
    {
        public OtherDocumentItem Reconstitute()
        {
            return new OtherDocumentItem(Id, Name, OtherDocumentId);
        }

        public void Update(OtherDocumentItem entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            OtherDocumentId = entity.OtherDocumentId;
        }
    }
}