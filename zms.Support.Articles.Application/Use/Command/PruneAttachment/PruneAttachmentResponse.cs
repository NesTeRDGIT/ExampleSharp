using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.PruneAttachment
{
    /// <summary>
    /// Результат команды очистки вложений не закрепленных за статьей, созданных более 48 часов назад
    /// </summary>
    public class PruneAttachmentResponse : IResponse
    {
        /// <summary>
        /// Количество удаленных вложений
        /// </summary>
        public long Count { get; set; }
    }
}
