using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.ClientQuery.GetAttachmentData
{
    /// <summary>
    /// Запрос данных вложения
    /// </summary>
    public class GetAttachmentDataQuery : IQuery<GetAttachmentDataResponse>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
    }
}
