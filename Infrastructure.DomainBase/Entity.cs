namespace Infrastructure.DomainBase
{
	/// <summary>
	///     Сущность
	/// </summary>
	public abstract class Entity : IEntity
	{
		protected Entity() : this(0)
		{
		}

		protected Entity(int id, bool deleted = false)
		{
			Id = id;
		    Deleted = deleted;
		}

		/// <summary>
		///     Идентификатор сущности
		/// </summary>
		public int Id { get; set; }

        public bool Deleted { get; set; }
	}
}