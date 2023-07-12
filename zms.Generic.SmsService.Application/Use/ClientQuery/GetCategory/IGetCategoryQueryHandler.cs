using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetCategory
{
    /// <summary>
    /// Обработчик запроса категорий сообщений
    /// </summary>
    public interface IGetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, GetCategoryResponse>
    {
    }
}
