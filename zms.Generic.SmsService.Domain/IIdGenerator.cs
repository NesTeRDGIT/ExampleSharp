using zms.Common.SharedKernel.Base.Domain;

namespace zms.Generic.SmsService.Domain
{
    /// <summary>
    /// Генератор идентификаторов
    /// </summary>
    public interface IIdGenerator
    {
        T NewId<T>() where T : IEntityId;
    }
}
