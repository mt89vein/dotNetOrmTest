using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider.Repositories
{
    public class SecondDocumentRepository : LinqRepository<SecondDocument, SecondDocumentDto>,
        ISecondDocumentRepository
    {
        protected override IQueryable<SecondDocumentDto> QueryAll => Table.Include(w => w.DocumentDto);
    }
}