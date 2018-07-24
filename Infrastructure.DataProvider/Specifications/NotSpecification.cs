namespace Infrastructure.DataProvider
{
	public class NotSpecification<T> : Specification<T>
	{
		public NotSpecification(ISpecification<T> specification) : base(specification.Predicate.Not())
		{
		}
	}
}