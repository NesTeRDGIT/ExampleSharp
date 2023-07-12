using zms.Common.Application.Base.Cqrs;

namespace zms.Generic.SmsService.Application.Use.Command.AddMessage
{
    /// <summary>
    /// Результат команды добавления сообщения
    /// </summary>
    public class AddMessageCommandResponse : IResponse
    {
        /// <summary>
        /// Идентификаторы
        /// </summary>
        public List<long> Id { get; set; }
    }
}
