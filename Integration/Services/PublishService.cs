using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Core;
using Integration.Mappings;
using Integration.PublishData;

namespace Integration.Services
{
    public class PublishService : IPublishService
    {
        public async Task<string> PublishAsync(IPublishable document, IPublishData data, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            using (var client = new HttpClient())
            {
                token.ThrowIfCancellationRequested();
                var message = await client.PostAsync("some url",
                    new StringContent(ToPublishFormat(document, data).ToString()), token);

                return message.Content.ToString();
            }
        }

        public void MarkAsPublished(IPublishable document, IPublishData data)
        {
            // mark as published logic
        }

        private static object ToPublishFormat(IPublishable document, IPublishData data)
        {
            switch (document)
            {
                case OtherDocument otherDocument:
                    return otherDocument.ToPublishFormat(data);
                // another documents
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}