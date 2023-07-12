namespace zms.Generic.SmsService.Application.Persistence.Repository
{
    /// <summary>
    /// Единица работы с хранилищем
    /// </summary>
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <returns></returns>
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
