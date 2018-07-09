using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Infrastructure.DataProvider.Repositories
{
    public class OtherDocumentPaymentRepository : LinqRepository<OtherDocumentPayment, OtherDocumentPaymentDto>,
        IOtherDocumentPaymentRepository
    {

        public IReadOnlyCollection<OtherDocumentPayment> GetOtherDocumentPaymentsByDocumentId(int documentId)
        {
            return QueryAll.Where(w => w.OtherDocumentId == documentId).Select(w => w.Reconstitute()).ToList();
        }

        protected override IQueryable<OtherDocumentPaymentDto> QueryAll => Table;
    }
}