using System;
using Domain;
using Domain.FetchStrategies;
using Domain.Services;
using Infrastructure.DataProvider.Caching;
using Infrastructure.DomainBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider.Services
{
    public class DocumentService : BaseService<Document, DocumentDto,ISpecification<DocumentDto>>, IDocumentService
    {
        private readonly IOtherDocumentService _otherDocumentService;
        public DocumentService(
            IRepository<Document, DocumentDto, ISpecification<DocumentDto>> repository,
            IOtherDocumentService otherDocumentService,
            ApplicationContext context, IRedisService<DocumentDto, Document> redisService
            )
            : base(repository, context, redisService)
        {
            _otherDocumentService = otherDocumentService;
        }

        public override void Update(Document entity, IWorkItemStrategy documentWorkItemStrategy = null)
        {
            switch (entity)
            {
                case OtherDocument otherDocument:
                    _otherDocumentService.Update(otherDocument, documentWorkItemStrategy);
                    break;
                // another document types...
            }

            throw new ArgumentOutOfRangeException(nameof(entity));
        }

        protected override ISpecification<DocumentDto> ToSpecification(
            IWorkItemStrategy documentWorkItemStrategy)
        {
            var specification = new Specification<DocumentDto>();

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

            specification.FetchStrategy.Add(w => w.Include(x => x.OtherDocumentDto));

            return specification;
        }
    }
}