namespace Infrastructure.DomainBase
{
    /// <summary>
    /// Сущность
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        int Id { get; }
    }
}