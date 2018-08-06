namespace Infrastructure.DataProvider
{
    public class OrNotSpecification<T> : CompositeSpecification<T> where T : class
    {
        public OrNotSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
            : base(leftSide.Predicate.OrNot(rightSide.Predicate))
        {
        }
    }
}