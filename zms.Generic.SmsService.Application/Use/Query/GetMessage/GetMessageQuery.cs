using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Query.GetMessage
{
    /// <summary>
    /// Запрос сообщений
    /// </summary>
    public class GetMessageQuery : IQuery<GetMessageResponse>
    {
        /// <summary>
        /// Список идентификаторов сообщений
        /// </summary>
        public List<long> Id { get; set; }
    }
}
