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

	    protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DocumentMap());
            builder.ApplyConfiguration(new OtherDocumentMap());
            builder.ApplyConfiguration(new OtherDocumentItemMap());
            builder.ApplyConfiguration(new OtherDocumentPaymentMap());
            builder.ApplyConfiguration(new SecondDocumentMap());

            base.OnModelCreating(builder);
        }

        public new bool SaveChanges()
        {
            const int retries = 3;
            var success = false;
            Validate();
            for (var i = 0; i < retries; i++)
            {
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
            {
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