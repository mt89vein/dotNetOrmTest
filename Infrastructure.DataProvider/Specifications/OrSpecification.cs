namespace Infrastructure.DataProvider
{
    public class OrSpecification<T> : CompositeSpecification<T> where T : class
    {
        public OrSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
            : base(leftSide.Predicate.Or(rightSide.Predicate))
        {
        }
    }
}