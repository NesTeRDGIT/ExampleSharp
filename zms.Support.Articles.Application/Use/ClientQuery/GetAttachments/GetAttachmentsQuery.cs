using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.ClientQuery.GetAttachments
{
    /// <summary>
    /// Запрос вложений
    /// </summary>
    public class GetAttachmentsQuery : IQuery<GetAttachmentsResponse>
    {
        /// <summary>
        /// Идентификатор статьи
        /// </summary>
        public long ArticleId { get; set; }
    }
}
