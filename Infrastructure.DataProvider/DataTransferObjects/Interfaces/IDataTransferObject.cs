namespace Infrastructure.DataProvider
{
    /// <summary>
    /// Интерфейс DTO для сущности
    /// </summary>
    /// <typeparam name="TDomainEntity">Тип сущности</typeparam>
    public interface IDataTransferObject<TDomainEntity>
    {
        int Id { get; set; }

        bool Deleted { get; set; }

        TDomainEntity Reconstitute();

        void Update(TDomainEntity entity);
    }
}