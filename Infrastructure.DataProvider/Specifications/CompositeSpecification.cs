using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider
{
    public abstract class CompositeSpecification<T> : ISpecification<T> where T : class
    {
        protected CompositeSpecification(Expression<Func<T, bool>> predicate)
        {
            FetchStrategy = new GenericFetchStrategy<T>();
            Predicate = predicate;
        }

        public ISpecification<T> And(ISpecification<T> specification)
        {
            return new Specification<T>(Predicate.And(specification.Predicate));
        }

        public ISpecification<T> And(Expression<Func<T, bool>> predicate)
        {
            return new Specification<T>(Predicate.And(predicate));
        }

        public ISpecification<T> AndAlso(ISpecification<T> specification)
        {
            return new Specification<T>(Predicate.AndAlso(specification.Predicate));
        }

        public ISpecification<T> AndAlso(Expression<Func<T, bool>> predicate)
        {
            return new Specification<T>(Predicate.AndAlso(predicate));
        }

        public ISpecification<T> Not()
        {
            return new Specification<T>(Predicate.Not());
        }

        public ISpecification<T> AndNot(ISpecification<T> specification)
        {
            return new Specification<T>(Predicate.AndNot(specification.Predicate));
        }

        public ISpecification<T> AndNot(Expression<Func<T, bool>> predicate)
        {
            return new Specification<T>(Predicate.AndNot(predicate));
        }

        public ISpecification<T> OrNot(ISpecification<T> specification)
        {
            return new Specification<T>(Predicate.OrNot(specification.Predicate));
        }

        public ISpecification<T> OrNot(Expression<Func<T, bool>> predicate)
        {
            return new Specification<T>(Predicate.OrNot(predicate));
        }

        public ISpecification<T> Or(ISpecification<T> specification)
        {
            return new Specification<T>(Predicate.Or(specification.Predicate));
        }

        public ISpecification<T> Or(Expression<Func<T, bool>> predicate)
        {
            return new Specification<T>(Predicate.Or(predicate));
        }

        public ISpecification<T> OrElse(ISpecification<T> specification)
        {
            return new Specification<T>(Predicate.OrElse(specification.Predicate));
        }

        public ISpecification<T> OrElse(Expression<Func<T, bool>> predicate)
        {
            return new Specification<T>(Predicate.OrElse(predicate));
        }

        #region ISpecification<T> Members

        public Expression<Func<T, bool>> Predicate { get; set; }

        public T SatisfyingEntityFrom(IQueryable<T> query)
        {
            return SatisfyingEntitiesFrom(query).FirstOrDefault();
        }

        public IQueryable<T> SatisfyingEntitiesFrom(IQueryable<T> query)
        {
            return Predicate == null ? query : query.Where(Predicate);
        }

        public bool IsSatisfiedBy(T entity)
        {
            if (Predicate == null)
            {
                return true;
            }

            return new[] {entity}.AsQueryable().Any(Predicate);
        }

        public IFetchStrategy<DbSet<T>> FetchStrategy { get; set; }

        #endregion
    }
}