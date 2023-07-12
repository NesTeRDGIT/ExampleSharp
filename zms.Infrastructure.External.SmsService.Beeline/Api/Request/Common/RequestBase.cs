using System.Text.Json.Serialization;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Request.Common
{
    /// <summary>
    /// Базовый класс запроса
    /// </summary>
    public abstract class RequestBase
    {
        protected RequestBase(string userName, string userPassword, string action)
        {
            UserName = userName;
            UserPassword = userPassword;
            Action = action;
        }

        /// <summary>
        /// Имя входа
        /// </summary>
        [JsonPropertyName("user")]
        [JsonPropertyOrder(-3)]
        public string UserName { get; }

        /// <summary>
        /// Пароль
        /// </summary>
        [JsonPropertyName("pass")]
        [JsonPropertyOrder(-2)]
        public string UserPassword { get; }

        /// <summary>
        /// Действие
        /// </summary>
        [JsonPropertyName("action")]
        [JsonPropertyOrder(-1)]
        public string Action { get; }
    }
}
