using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataProvider
{
    public class Specification<T> : ISpecification<T>
        where T : class
    {
        public Specification()
            : this((Expression<Func<T, bool>>)null)
        {
        }

        public Specification(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
            FetchStrategy = new GenericFetchStrategy<T>();
        }

        public Specification(ISpecification<T> specification)
        {
            Predicate = specification.Predicate;
            FetchStrategy = specification.FetchStrategy;
        }

        public ISpecification<T> And(ISpecification<T> specification)
        {
            return Instanciate(Predicate.And(specification.Predicate),
                InstanciateFetchStrategy(specification.FetchStrategy));
        }

        public ISpecification<T> And(Expression<Func<T, bool>> predicate)
        {
            return Instanciate(Predicate.And(predicate), FetchStrategy);
        }

        public ISpecification<T> AndAlso(ISpecification<T> specification)
        {
            return Instanciate(Predicate.AndAlso(specification.Predicate),
                InstanciateFetchStrategy(specification.FetchStrategy));
        }

        public ISpecification<T> AndAlso(Expression<Func<T, bool>> predicate)
        {
            return Instanciate(Predicate.AndAlso(predicate), FetchStrategy);
        }

        public ISpecification<T> Not()
        {
            return Instanciate(Predicate.Not(), FetchStrategy);
        }

        public ISpecification<T> AndNot(ISpecification<T> specification)
        {
            return Instanciate(Predicate.AndNot(specification.Predicate),
                InstanciateFetchStrategy(specification.FetchStrategy));
        }

        public ISpecification<T> AndNot(Expression<Func<T, bool>> predicate)
        {
            return Instanciate(Predicate.AndNot(predicate), FetchStrategy);
        }

        public ISpecification<T> OrNot(ISpecification<T> specification)
        {
            return Instanciate(Predicate.OrNot(specification.Predicate),
                InstanciateFetchStrategy(specification.FetchStrategy));
        }

        public ISpecification<T> OrNot(Expression<Func<T, bool>> predicate)
        {
            return Instanciate(Predicate.OrNot(predicate), FetchStrategy);
        }

        public ISpecification<T> Or(ISpecification<T> specification)
        {
            return Instanciate(Predicate.Or(specification.Predicate),
                InstanciateFetchStrategy(specification.FetchStrategy));
        }

        public ISpecification<T> Or(Expression<Func<T, bool>> predicate)
        {
            return Instanciate(Predicate.Or(predicate), FetchStrategy);
        }

        public ISpecification<T> OrElse(ISpecification<T> specification)
        {
            return Instanciate(Predicate.OrElse(specification.Predicate),
                InstanciateFetchStrategy(specification.FetchStrategy));
        }

        public ISpecification<T> OrElse(Expression<Func<T, bool>> predicate)
        {
            return Instanciate(Predicate.OrElse(predicate), FetchStrategy);
        }

        protected virtual Specification<T> Instanciate(Expression<Func<T, bool>> predicate,
            IFetchStrategy<DbSet<T>> strategy = null)
        {
            var specification = new Specification<T>(predicate);
            if (strategy != null)
            {
                specification.FetchStrategy = strategy;
            }

            return specification;
        }

        protected IFetchStrategy<DbSet<T>> InstanciateFetchStrategy(IFetchStrategy<DbSet<T>> strategy)
        {
            var thisPaths = FetchStrategy != null ? FetchStrategy.IncludePaths : new List<string>();
            var paramPaths = strategy != null ? strategy.IncludePaths : new List<string>();
            var includePaths = thisPaths.Union(paramPaths);

            var newStrategy = new GenericFetchStrategy<T>();
            foreach (var includePath in includePaths) newStrategy.Add(includePath);

            return newStrategy;
        }

        public static ISpecification<T> And(ISpecification<T> specification, ISpecification<T> specification2)
        {
            return new Specification<T>(specification.And(specification2));
        }

        public static ISpecification<T> And(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> predicate2)
        {
            return new Specification<T>(predicate.And(predicate2));
        }

        public static ISpecification<T> AndAlso(ISpecification<T> specification, ISpecification<T> specification2)
        {
            return new Specification<T>(specification.AndAlso(specification));
        }

        public static ISpecification<T> AndAlso(Expression<Func<T, bool>> predicate,
            Expression<Func<T, bool>> predicate2)
        {
            return new Specification<T>(predicate.AndAlso(predicate2));
        }

        public static ISpecification<T> Not(ISpecification<T> specification)
        {
            return new Specification<T>(specification.Not());
        }

        public static ISpecification<T> AndNot(ISpecification<T> specification, ISpecification<T> specification2)
        {
            return new Specification<T>(specification.AndNot(specification2));
        }

        public static ISpecification<T> AndNot(Expression<Func<T, bool>> predicate,
            Expression<Func<T, bool>> predicate2)
        {
            return new Specification<T>(predicate.AndNot(predicate2));
        }

        public static ISpecification<T> OrNot(ISpecification<T> specification, ISpecification<T> specification2)
        {
            return new Specification<T>(specification.OrNot(specification2));
        }

        public static ISpecification<T> OrNot(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> predicate2)
        {
            return new Specification<T>(predicate.OrNot(predicate2));
        }

        public static ISpecification<T> Or(ISpecification<T> specification, ISpecification<T> specification2)
        {
            return new Specification<T>(specification.Or(specification2));
        }

        public static ISpecification<T> Or(Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> predicate2)
        {
            return new Specification<T>(predicate.Or(predicate2));
        }

        public static ISpecification<T> OrElse(ISpecification<T> specification, ISpecification<T> specification2)
        {
            return new Specification<T>(specification.OrElse(specification2));
        }

        public static ISpecification<T> OrElse(Expression<Func<T, bool>> predicate,
            Expression<Func<T, bool>> predicate2)
        {
            return new Specification<T>(predicate.OrElse(predicate2));
        }

        #region ISpecification<T> Members

        public Expression<Func<T, bool>> Predicate { get; set; }

        public IFetchStrategy<DbSet<T>> FetchStrategy { get; set; }

        public virtual T SatisfyingEntityFrom(IQueryable<T> query)
        {
            return SatisfyingEntitiesFrom(query).FirstOrDefault();
        }

        public virtual IQueryable<T> SatisfyingEntitiesFrom(IQueryable<T> query)
        {
            return Predicate == null ? query : query.Where(Predicate);
        }

        public bool IsSatisfiedBy(T entity)
        {
            return Predicate == null || new[] {entity}.AsQueryable().Any(Predicate);
        }

        #endregion
    }
}