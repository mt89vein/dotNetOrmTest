using System;
using System.Collections.Generic;
using Domain;
using Domain.FetchStrategies;
using Domain.Services;
using Infrastructure.DataProvider.Caching;
using Infrastructure.DomainBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider.Services
{
    public class DocumentService : BaseService<Document, DocumentDto, ISpecification<DocumentDto>>, IDocumentService
    {
        private readonly IOtherDocumentService _otherDocumentService;

        public DocumentService(
            IRepository<Document, DocumentDto, ISpecification<DocumentDto>> repository,
            IOtherDocumentService otherDocumentService,
            ApplicationContext context, 
            ICacheService<DocumentDto, Document> cacheService)
            : base(repository, context, cacheService)
        {
            _otherDocumentService = otherDocumentService;
        }

        public override void Save(Document entity, IWorkItemStrategy documentWorkItemStrategy = null)
        {
            switch (entity)
            {
                case OtherDocument otherDocument:
                    _otherDocumentService.Save(otherDocument, documentWorkItemStrategy);
                    break;
                // another document types...
            }

            throw new ArgumentOutOfRangeException(nameof(entity));
        }

        public override void Remove(Document entity)
        {
            switch (entity)
            {
                case OtherDocument otherDocument:
                    _otherDocumentService.Remove(otherDocument);
                    break;
                // another document types...
            }

            throw new ArgumentOutOfRangeException(nameof(entity));
        }

        public override Document Get(int id, IWorkItemStrategy workItemStrategy = null)
        {
            return base.Get(id, workItemStrategy ?? new DocumentWorkItemStrategy());
        }

        public override IReadOnlyCollection<Document> Get(IWorkItemStrategy workItemStrategy = null)
        {
            return base.Get(workItemStrategy ?? new DocumentWorkItemStrategy());
        }

        public override IReadOnlyCollection<Document> Get(IEnumerable<int> ids, IWorkItemStrategy workItemStrategy = null)
        {
            return base.Get(ids, workItemStrategy ?? new DocumentWorkItemStrategy());
        }

        protected override ISpecification<DocumentDto> ToSpecification(IWorkItemStrategy documentWorkItemStrategy)
        {
            var specification = new Specification<DocumentDto>();

            specification.FetchStrategy.Add(w => w.Include(x => x.OtherDocumentDto));
            // add Inlcude all inherited types..

            if (!(documentWorkItemStrategy is DocumentWorkItemStrategy strategy))
            {
                return specification;
            }

            if (strategy.WithAttachments)
            {
                specification.FetchStrategy.Add(
                    w => w.Include(x => x.AttachmentLinkDtos)
                        .ThenInclude(x => x.AttachmentDto)
                );
            }

            if (!strategy.WithDeleted)
            {
                specification.And(OnlyNotDeletedSpecification);
            }

            return specification;
        }
    }
}
 