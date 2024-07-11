using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.ClientQuery.GetAttachments
{
    /// <summary>
    /// Обработчик запроса вложений
    /// </summary>
    public interface IGetAttachmentsQueryHandler : IQueryHandler<GetAttachmentsQuery, GetAttachmentsResponse>;
}
