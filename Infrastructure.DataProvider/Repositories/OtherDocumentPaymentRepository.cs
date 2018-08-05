using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Infrastructure.DataProvider.Repositories
{
    public class OtherDocumentPaymentRepository : LinqRepository<OtherDocumentPayment, OtherDocumentPaymentDto,
            ISpecification<OtherDocumentPaymentDto>>,
        IOtherDocumentPaymentRepository
    {
        public OtherDocumentPaymentRepository(ApplicationContext context) : base(context)
        {
        }

        public IReadOnlyCollection<OtherDocumentPaymentDto> GetOtherDocumentPaymentsByDocumentId(int documentId,
            ISpecification<OtherDocumentPaymentDto> specification = null)
        {
            return BaseQuery(specification)
                .Where(w => w.OtherDocumentId == documentId)
                .ToList();
        }
    }
}