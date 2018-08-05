using System.Collections.Generic;
using Domain;
using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider.Repositories
{
    public interface
        IOtherDocumentPaymentRepository : IRepository<OtherDocumentPayment, OtherDocumentPaymentDto,
            ISpecification<OtherDocumentPaymentDto>>
    {
        IReadOnlyCollection<OtherDocumentPaymentDto> GetOtherDocumentPaymentsByDocumentId(int documentId,
            ISpecification<OtherDocumentPaymentDto> specification = null);
    }
}