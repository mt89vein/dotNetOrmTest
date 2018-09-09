using System;

namespace Domain.FetchStrategies
{
    public class OtherDocumentWorkItemStrategy : DocumentWorkItemStrategy
    {
        public OtherDocumentWorkItemStrategy(
            bool withDeleted = false,
            bool withAttachments = true,
            bool withPayments = true,
            bool withItems = true,
            bool cacheResult = false,
            OtherDocumentItemWorkItemStrategy otherDocumentItemWorkItemStrategy = null,
            OtherDocumentPaymentWorkItemStrategy otherDocumentItemWorkPaymentStrategy = null
        )
            : base(withDeleted, withAttachments, cacheResult)
        {
            WithPayments = withPayments;
            WithItems = withItems;
            if (withItems && otherDocumentItemWorkItemStrategy == null)
            {
                throw new ArgumentNullException(nameof(otherDocumentItemWorkItemStrategy));
            }
            OtherDocumentItemWorkItemStrategy = otherDocumentItemWorkItemStrategy;
            if (withPayments && otherDocumentItemWorkPaymentStrategy == null)
            {
                throw new ArgumentNullException(nameof(otherDocumentItemWorkPaymentStrategy));
            }
            OtherDocumentItemWorkPaymentStrategy = otherDocumentItemWorkPaymentStrategy;
        }

        public bool WithPayments { get; }

        public bool WithItems { get; }

        public OtherDocumentItemWorkItemStrategy OtherDocumentItemWorkItemStrategy { get; }

        public OtherDocumentPaymentWorkItemStrategy OtherDocumentItemWorkPaymentStrategy { get; }
    }
}