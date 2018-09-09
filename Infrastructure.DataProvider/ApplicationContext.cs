using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Infrastructure.DataProvider.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DocumentDto> DocumentDtos { get; set; }

        public DbSet<AttachmentDto> AttachmentDtos { get; set; }

        public DbSet<AttachmentLinkDto> AttachmentLinkDtos { get; set; }

        public DbSet<OtherDocumentDto> OtherDocumentDtos { get; set; }

        public DbSet<OtherDocumentItemDto> OtherDocumentItemDto { get; set; }

        public DbSet<OtherDocumentPaymentDto> OtherDocumentPaymentDto { get; set; }

        public DbSet<OneMoreNestedItemDto> OneMoreNestedItemDtos { get; set; }

        public DbSet<NestedItemDto> NestedItemDtos { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions)
            : base(contextOptions)
        {
        }

        private void Seed(ModelBuilder builder)
        {
            builder.Entity<DocumentDto>().HasData(
                new DocumentDto
                {
                    Deleted = false,
                    DocumentType = 0,
                    Id = 1,
                    Name = "Первый документ первого типа"
                },
                new DocumentDto
                {
                    Deleted = true,
                    DocumentType = 0,
                    Id = 2,
                    Name = "Второй удаленный документ первого типа"
                },
                new DocumentDto
                {
                    Deleted = false,
                    DocumentType = 0,
                    Id = 3,
                    Name = "Третий документ первого типа"
                },
                new DocumentDto
                {
                    Deleted = false,
                    DocumentType = 0,
                    Id = 4,
                    Name = "Четвертый документ первого типа"
                }
            );

            builder.Entity<AttachmentDto>().HasData(
                new AttachmentDto
                {
                    Id = 1,
                    Deleted = false,
                    Path = "some/path"
                },
                new AttachmentDto
                {
                    Id = 2,
                    Deleted = false,
                    Path = "some/another/path"
                },
                new AttachmentDto
                {
                    Id = 3,
                    Deleted = true,
                    Path = "some/either/path"
                });

            builder.Entity<AttachmentLinkDto>().HasData(
                new AttachmentLinkDto
                {
                    Id = 1,
                    AttachmentId = 1,
                    DocumentId = 1,
                },
                new AttachmentLinkDto
                {
                    Id = 2,
                    AttachmentId = 2,
                    DocumentId = 1,
                },
                new AttachmentLinkDto
                {
                    Id = 3,
                    AttachmentId = 3,
                    DocumentId = 2,
                },
                new AttachmentLinkDto
                {
                    Id = 4,
                    AttachmentId = 1,
                    DocumentId = 2,
                },
                new AttachmentLinkDto
                {
                    Id = 5,
                    AttachmentId = 2,
                    DocumentId = 3,
                }
            );

            builder.Entity<OtherDocumentDto>().HasData(
                new OtherDocumentDto
                {
                    Deleted = false,
                    Id = 1,
                    TestName = "Первый документ первого типа"
                },
                new OtherDocumentDto
                {
                    Deleted = true,
                    Id = 2,
                    TestName = "Второй документ первого типа"
                },
                new OtherDocumentDto
                {
                    Deleted = false,
                    Id = 3,
                    TestName = "Третий удаленный документ первого типа"
                },
                new OtherDocumentDto
                {
                    Deleted = false,
                    Id = 4,
                    TestName = "Четвертый документ первого типа"
                }
            );

            builder.Entity<OtherDocumentItemDto>().HasData(
                new OtherDocumentItemDto
                {
                    Deleted = false,
                    Id = 1,
                    Name = "item 1 1",
                    OtherDocumentId = 1
                },
                new OtherDocumentItemDto
                {
                    Deleted = true,
                    Id = 2,
                    Name = "item 1 2",
                    OtherDocumentId = 1
                },
                new OtherDocumentItemDto
                {
                    Deleted = false,
                    Id = 3,
                    Name = "item 2 1",
                    OtherDocumentId = 2
                },
                new OtherDocumentItemDto
                {
                    Deleted = true,
                    Id = 4,
                    Name = "item 2 2",
                    OtherDocumentId = 2
                },
                new OtherDocumentItemDto
                {
                    Deleted = false,
                    Id = 5,
                    Name = "item 3 1",
                    OtherDocumentId = 3
                },
                new OtherDocumentItemDto
                {
                    Deleted = true,
                    Id = 6,
                    Name = "item 3 2",
                    OtherDocumentId = 3
                },
                new OtherDocumentItemDto
                {
                    Deleted = false,
                    Id = 7,
                    Name = "item 4 1",
                    OtherDocumentId = 4
                },
                new OtherDocumentItemDto
                {
                    Deleted = true,
                    Id = 8,
                    Name = "item 4 2",
                    OtherDocumentId = 4
                }
            );

            builder.Entity<OtherDocumentPaymentDto>().HasData(
                new OtherDocumentPaymentDto
                {
                    Deleted = false,
                    Id = 1,
                    Total = "150",
                    OtherDocumentId = 1
                },
                new OtherDocumentPaymentDto
                {
                    Deleted = true,
                    Id = 2,
                    Total = "25",
                    OtherDocumentId = 1
                },
                new OtherDocumentPaymentDto
                {
                    Deleted = false,
                    Id = 3,
                    Total = "450",
                    OtherDocumentId = 2
                },
                new OtherDocumentPaymentDto
                {
                    Deleted = true,
                    Id = 4,
                    Total = "132",
                    OtherDocumentId = 2
                },
                new OtherDocumentPaymentDto
                {
                    Deleted = false,
                    Id = 5,
                    Total = "444",
                    OtherDocumentId = 3
                },
                new OtherDocumentPaymentDto
                {
                    Deleted = true,
                    Id = 6,
                    Total = "521",
                    OtherDocumentId = 3
                },
                new OtherDocumentPaymentDto
                {
                    Deleted = false,
                    Id = 7,
                    Total = "421",
                    OtherDocumentId = 4
                },
                new OtherDocumentPaymentDto
                {
                    Deleted = true,
                    Id = 8,
                    Total = "4444",
                    OtherDocumentId = 4
                }
            );

            builder.Entity<NestedItemDto>().HasData(
                new NestedItemDto
                {
                    Id = 1,
                    Deleted = false,
                    NestedItemName = "nested item name 1",
                    OtherDocumentItemId = 1
                },
                new NestedItemDto
                {
                    Id = 2,
                    Deleted = false,
                    NestedItemName = "nested item name 2",
                    OtherDocumentItemId = 1
                },
                new NestedItemDto
                {
                    Id = 3,
                    Deleted = false,
                    NestedItemName = "nested item name 3",
                    OtherDocumentItemId = 2
                },
                new NestedItemDto
                {
                    Id = 4,
                    Deleted = false,
                    NestedItemName = "nested item name 4",
                    OtherDocumentItemId = 2
                },
                new NestedItemDto
                {
                    Id = 5,
                    Deleted = false,
                    NestedItemName = "nested item name 5",
                    OtherDocumentItemId = 3
                },
                new NestedItemDto
                {
                    Id = 6,
                    Deleted = false,
                    NestedItemName = "nested item name 6",
                    OtherDocumentItemId = 3
                },
                new NestedItemDto
                {
                    Id = 7,
                    Deleted = false,
                    NestedItemName = "nested item name 7",
                    OtherDocumentItemId = 4
                },
                new NestedItemDto
                {
                    Id = 8,
                    Deleted = false,
                    NestedItemName = "nested item name 8",
                    OtherDocumentItemId = 4
                },
                new NestedItemDto
                {
                    Id = 9,
                    Deleted = false,
                    NestedItemName = "nested item name 9",
                    OtherDocumentItemId = 8
                }
            );
            
            builder.Entity<OneMoreNestedItemDto>().HasData(
                new OneMoreNestedItemDto
                {
                    Id = 1,
                    OneMoreNestedItemName = "one more nested item name 1",
                    NestedItemId = 1
                },
                new OneMoreNestedItemDto
                {
                    Id = 2,
                    OneMoreNestedItemName = "one more nested item name 2",
                    NestedItemId = 1
                },
                new OneMoreNestedItemDto
                {
                    Id = 3,
                    OneMoreNestedItemName = "one more nested item name 3",
                    NestedItemId = 2
                },
                new OneMoreNestedItemDto
                {
                    Id = 4,
                    OneMoreNestedItemName = "one more nested item name 4",
                    NestedItemId = 2
                },
                new OneMoreNestedItemDto
                {
                    Id = 5,
                    OneMoreNestedItemName = "one more nested item name 5",
                    NestedItemId = 3
                },
                new OneMoreNestedItemDto
                {
                    Id = 6,
                    OneMoreNestedItemName = "one more nested item name 6",
                    NestedItemId = 3
                },
                new OneMoreNestedItemDto
                {
                    Id = 7,
                    OneMoreNestedItemName = "one more nested item name 7",
                    NestedItemId = 4
                },
                new OneMoreNestedItemDto
                {
                    Id = 8,
                    OneMoreNestedItemName = "one more nested item name 8",
                    NestedItemId = 4
                },
                new OneMoreNestedItemDto
                {
                    Id = 9,
                    OneMoreNestedItemName = "one more nested item name 9",
                    NestedItemId = 5
                },
                new OneMoreNestedItemDto
                {
                    Id = 10,
                    OneMoreNestedItemName = "one more nested item name 10",
                    NestedItemId = 5
                },
                new OneMoreNestedItemDto
                {
                    Id = 11,
                    OneMoreNestedItemName = "one more nested item name 11",
                    NestedItemId = 6
                },
                new OneMoreNestedItemDto
                {
                    Id = 12,
                    OneMoreNestedItemName = "one more nested item name 12",
                    NestedItemId = 6
                },
                new OneMoreNestedItemDto
                {
                    Id = 13,
                    OneMoreNestedItemName = "one more nested item name 13",
                    NestedItemId = 7
                },
                new OneMoreNestedItemDto
                {
                    Id = 14,
                    OneMoreNestedItemName = "one more nested item name 14",
                    NestedItemId = 7
                },
                new OneMoreNestedItemDto
                {
                    Id = 15,
                    OneMoreNestedItemName = "one more nested item name 15",
                    NestedItemId = 8
                },
                new OneMoreNestedItemDto
                {
                    Id = 16,
                    OneMoreNestedItemName = "one more nested item name 16",
                    NestedItemId = 8
                }
            );
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AttachmentMap());
            builder.ApplyConfiguration(new AttachmentLinkMap());
            builder.ApplyConfiguration(new DocumentMap());
            builder.ApplyConfiguration(new OtherDocumentMap());
            builder.ApplyConfiguration(new OtherDocumentItemMap());
            builder.ApplyConfiguration(new OtherDocumentPaymentMap());
            builder.ApplyConfiguration(new NestedItemMap());
            builder.ApplyConfiguration(new OneMoreNestedItemMap());

            Seed(builder);
            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Валидировать по схеме базы данных
        /// </summary>
        /// <exception cref="ValidationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        private void Validate()
        {
            var entities = from e in ChangeTracker.Entries()
                where e.State == EntityState.Added
                      || e.State == EntityState.Modified
                select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }
        }
    }
}