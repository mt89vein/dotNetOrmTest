namespace Infrastructure.DomainBase
{
    /// <summary>
    /// Сущность
    /// </summary>
    public abstract class Entity : IEntity
    {
        protected Entity() : this(0)
        {
        }

        protected Entity(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public int Id { get; set; }
    }
}