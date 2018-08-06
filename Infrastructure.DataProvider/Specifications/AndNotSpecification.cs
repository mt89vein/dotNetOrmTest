namespace Infrastructure.DataProvider
{
    public class AndNotSpecification<T> : CompositeSpecification<T> where T: class
    {
        public AndNotSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
            : base(leftSide.Predicate.AndNot(rightSide.Predicate))
        {
        }
    }
}