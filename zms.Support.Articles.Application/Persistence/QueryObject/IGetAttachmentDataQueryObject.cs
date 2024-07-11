using zms.Support.Articles.Application.Use.ClientQuery.GetAttachmentData;
using zms.Support.Articles.Domain.OfAttachment;

namespace zms.Support.Articles.Application.Persistence.QueryObject
{
    /// <summary>
    /// Объект запроса данных вложения
    /// </summary>
    public interface IGetAttachmentDataQueryObject
    {
        /// <summary>
        /// Получить
        /// </summary>
        /// <param name="attachmentId">Идентификатор</param>
        /// <returns></returns>
        Task<AttachmentDataProjection> GetAsync(AttachmentId attachmentId);
    }
}
