namespace Domain.Core
{
    /// <summary>
    /// Возможные состояния публикации
    /// </summary>
    public enum PublishState
    {
        Queued,
        Dispatching,
        Processing,
        Published
    }
}