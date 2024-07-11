using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.ClientQuery.GetAttachmentData
{
    /// <summary>
    /// Обработчик запроса данных вложения
    /// </summary>
    public interface IGetAttachmentDataQueryHandler : IQueryHandler<GetAttachmentDataQuery, GetAttachmentDataResponse>;
}
