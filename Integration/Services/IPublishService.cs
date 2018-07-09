using System.Threading;
using System.Threading.Tasks;
using Domain.Core;
using Integration.PublishData;

namespace Integration.Services
{
    public interface IPublishService
    {
        Task<string> PublishAsync(IPublishable document, IPublishData data, CancellationToken token);

        void MarkAsPublished(IPublishable document, IPublishData data);
    }
}