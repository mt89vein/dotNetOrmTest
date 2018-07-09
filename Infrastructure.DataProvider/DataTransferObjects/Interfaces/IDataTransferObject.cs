using Infrastructure.DomainBase;

namespace Infrastructure.DataProvider
{
    /// <summary>
    /// Интерфейс DTO для сущности
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public interface IDataTransferObject<TEntity>
        where TEntity : IEntity
    {
        int Id { get; set; }

        TEntity Reconstitute();

        void Update(TEntity entity);
    }
}