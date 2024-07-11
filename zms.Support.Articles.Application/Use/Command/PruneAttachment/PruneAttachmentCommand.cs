using zms.Common.Application.Base.Cqrs;

namespace zms.Support.Articles.Application.Use.Command.PruneAttachment
{
    /// <summary>
    /// Команда очистки вложений не закрепленных за статьей, созданных более 48 часов назад
    /// </summary>
    public class PruneAttachmentCommand : ICommand<PruneAttachmentResponse>
    {
    }
}
