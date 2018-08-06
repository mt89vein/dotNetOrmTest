namespace Infrastructure.DataProvider
{
    public class OrElseSpecification<T> : CompositeSpecification<T> where T : class
    {
        public OrElseSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
            : base(leftSide.Predicate.OrElse(rightSide.Predicate))
        {
        }
    }
}