using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.DataProvider.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider
{
    public class ApplicationContext : DbContext
    {
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
                    DocumentId = 1,
                    Path = "some/path"
                },
                new AttachmentDto
                {
                    Id = 2,
                    Deleted = false,
                    DocumentId = 1,
                    Path = "some/another/path"
                },
                new AttachmentDto
                {
                    Id = 3,
                    Deleted = true,
                    DocumentId = 1,
                    Path = "some/either/path"
                });

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
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AttachmentMap());
            builder.ApplyConfiguration(new DocumentMap());
            builder.ApplyConfiguration(new OtherDocumentMap());
            builder.ApplyConfiguration(new OtherDocumentItemMap());
            builder.ApplyConfiguration(new OtherDocumentPaymentMap());

            Seed(builder);
            base.OnModelCreating(builder);
        }

        public new bool SaveChanges()
        {
            const int retries = 3;
            var success = false;
            Validate();
            for (var i = 0; i < retries; i++)
                using (var transaction = Database.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        base.SaveChanges(false);
                        transaction.Commit();
                        success = true;
                        break;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        success = false;
                    }
                }

            if (!success)
            {
                return false;
            }

            ChangeTracker.AcceptAllChanges();
            return true;
        }

        public new async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            const int retries = 3;
            var success = false;
            Validate();
            for (var i = 0; i < retries; i++)
                using (var transaction = Database.BeginTransaction())
                {
                    try
                    {
                        await base.SaveChangesAsync(false, cancellationToken);
                        transaction.Commit();
                        success = true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        success = false;
                    }
                }

            if (!success)
            {
                return false;
            }

            ChangeTracker.AcceptAllChanges();
            return true;
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