namespace Infrastructure.DataProvider
{
    public class NotSpecification<T> : Specification<T> where T : class
    {
        public NotSpecification(ISpecification<T> specification) : base(specification.Predicate.Not())
        {
        }
    }
}