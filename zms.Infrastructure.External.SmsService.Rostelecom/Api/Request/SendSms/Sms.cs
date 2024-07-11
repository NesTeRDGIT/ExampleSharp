using zms.Common.SharedKernel.Exception;

namespace zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.SendSms
{
    /// <summary>
    /// СМС
    /// </summary>
    public class Sms
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">Текст СМС</param>
        /// <param name="target">Получатель СМС</param>
        /// <exception cref="BadValueDomainException"></exception>
        public Sms(string text, string target)
        {
            Text = string.IsNullOrEmpty(text) ? throw new BadValueDomainException("Текст сообщения не может быть пустым") : text;
            Target = string.IsNullOrEmpty(target) ? throw new BadValueDomainException("Получатель не может быть пустым") : target;
        }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Получатель
        /// </summary>
        public string Target { get; }
    }
}
