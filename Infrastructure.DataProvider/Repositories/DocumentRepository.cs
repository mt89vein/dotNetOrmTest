using System;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider.Repositories
{
    public class DocumentRepository : LinqRepository<Document, DocumentDto>, IDocumentRepository
    {
        private readonly IOtherDocumentRepository _otherDocumentRepository;
        private readonly ISecondDocumentRepository _secondDocumentRepository;

        public DocumentRepository(IOtherDocumentRepository otherDocumentRepository,
            ISecondDocumentRepository secondDocumentRepository)
        {
            _otherDocumentRepository = otherDocumentRepository;
            _secondDocumentRepository = secondDocumentRepository;
        }

        protected override IQueryable<DocumentDto> QueryAll => Table.Include(w => w.OtherDocumentDto).Include(w => w.SecondDocumentDto);

        public override bool Save(Document entity)
        {
            if (entity is OtherDocument otherDocument)
            {
                return _otherDocumentRepository.Save(otherDocument);
            }

            if (entity is SecondDocument secondDocument)
            {
                return _secondDocumentRepository.Save(secondDocument);
            }

            throw new Exception("Неизвестный тип документа");
        }
    }
}