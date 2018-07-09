using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider.Repositories
{
    public class OtherDocumentRepository : LinqRepository<OtherDocument, OtherDocumentDto>,
        IOtherDocumentRepository
    {
        protected override IQueryable<OtherDocumentDto> QueryAll => Table.Include(w => w.DocumentDto);
    }
}