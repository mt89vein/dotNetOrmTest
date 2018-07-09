using Domain;
using Integration.PublishData;

namespace Integration.Mappings
{
    public static class OtherDocumentPublishExtension
    {
        public static object ToPublishFormat(this OtherDocument document, IPublishData publishData)
        {
            var s = (OtherDocumentPublishData) publishData;

            return document + s.SomeExtraData;
        }
    }
}