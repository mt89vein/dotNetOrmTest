﻿namespace Infrastructure.DataProvider
{
    public class OrElseSpecification<T> : CompositeSpecification<T>
    {
        public OrElseSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
            : base(leftSide.Predicate.OrElse(rightSide.Predicate))
        {
        }
    }
}