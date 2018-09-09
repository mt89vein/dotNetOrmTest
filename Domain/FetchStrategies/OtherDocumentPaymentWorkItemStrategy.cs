using Infrastructure.DomainBase;

namespace Domain.FetchStrategies
{
	public class OtherDocumentPaymentWorkItemStrategy : IWorkItemStrategy
	{
		public OtherDocumentPaymentWorkItemStrategy(bool withDeleted = false, bool cacheResult = false)
		{
		    WithDeleted = withDeleted;
		    CacheResult = cacheResult;
        }

	    public bool WithDeleted { get; }

	    public bool CacheResult { get; }
	}
}