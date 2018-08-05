﻿namespace Infrastructure.DataProvider
{
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        public OrSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
            : base(leftSide.Predicate.Or(rightSide.Predicate))
        {
        }
    }
}