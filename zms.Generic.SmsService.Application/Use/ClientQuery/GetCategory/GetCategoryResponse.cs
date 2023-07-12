using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.ClientQuery.GetCategory
{
    /// <summary>
    /// Ответ на запрос категорий сообщений
    /// </summary>
    public class GetCategoryResponse : IResponse
    {
        /// <summary>
        /// Категории
        /// </summary>
        public IList<CategoryProjection> Categories { get; set; }
    }
}
