using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.ClientQuery.GetAttachmentData
{
    /// <summary>
    /// Ответ на запрос данных вложения
    /// </summary>
    public class GetAttachmentDataResponse : IResponse
    {
        /// <summary>
        /// Вложение
        /// </summary>
        public AttachmentDataProjection Attachment { get; set; }
    }
}
