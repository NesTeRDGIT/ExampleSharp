using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.ClientQuery.GetAttachments
{
    /// <summary>
    /// Ответ на запрос вложений
    /// </summary>
    public class GetAttachmentsResponse : IResponse
    {
        /// <summary>
        /// Вложения
        /// </summary>
        public IList<AttachmentProjection> Attachments { get; set; }
    }
}
