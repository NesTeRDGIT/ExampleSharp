using System.Text.Json.Serialization;

namespace zms.Infrastructure.External.SmsService.Beeline.Api.Request.GetStatus
{
    /// <summary>
    /// Информация о статусе СМС
    /// </summary>
    public class StatusInfo
    {
        public StatusInfo(string smsId)
        {
            SmsId = smsId;
        }

        /// <summary>
        /// ID рассылки сообщений
        /// </summary>
        [JsonPropertyName("sms_group_id")]
        [JsonPropertyOrder(1)]
        public string SmsGroupId { get; set; }

        /// <summary>
        /// ID сообщения
        /// </summary>
        [JsonPropertyName("sms_id")]
        [JsonPropertyOrder(2)]
        public string SmsId { get; set; }


        /// <summary>
        /// Тип сообщения
        /// </summary>
        [JsonPropertyName("sms_type")]
        [JsonPropertyOrder(3)]
        public string SmsType { get; set; }

        /// <summary>
        /// Дата и время создания сообщения
        /// </summary>
        [JsonPropertyName("created")]
        [JsonPropertyOrder(4)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Имя пользователя, создавшего сообщение
        /// </summary>
        [JsonPropertyName("aul_username")]
        [JsonPropertyOrder(5)]
        public string UserName { get; set; }


        /// <summary>
        /// IP адрес пользователя, создавшего сообщение
        /// </summary>
        [JsonPropertyName("aul_client_addr")]
        [JsonPropertyOrder(6)]
        public string ClientAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("aul_proxy_addr")]
        [JsonPropertyOrder(7)]
        public string ProxyAddress { get; set; }

        /// <summary>
        /// Телефон адресата
        /// </summary>
        [JsonPropertyName("target")]
        [JsonPropertyOrder(8)]
        public string Target { get; set; }


        /// <summary>
        /// Имя отправителя сообщения
        /// </summary>
        [JsonPropertyName("sender")]
        [JsonPropertyOrder(9)]
        public string Sender { get; set; }

        /// <summary>
        /// Кол-во единиц ресурсов на данное сообщение
        /// </summary>
        [JsonIgnore]
        public int SmsCount { get; set; }

        [JsonPropertyName("sms_count")]
        [JsonPropertyOrder(10)]
        public string SmsCountString
        {
            get => SmsCount.ToString();
            set => SmsCount = Convert.ToInt32(value);
        }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        [JsonPropertyName("text")]
        [JsonPropertyOrder(11)]
        public string Text { get; set; }

        /// <summary>
        /// Код статуса доставки сообщения
        /// </summary>
        [JsonPropertyName("stc_code")]
        [JsonPropertyOrder(12)]
        public string StatusCode { get; set; }

        /// <summary>
        /// 0 - сообщение не отослано, 1 = сообщение отослано успешно
        /// </summary>
        [JsonPropertyName("sent")]
        [JsonPropertyOrder(13)]
        public string Sent { get; set; }

        /// <summary>
        /// 0 - сообщения находится в процессинге, 1 = работа по отправке сообщения завершена
        /// </summary>
        [JsonPropertyName("closed")]
        [JsonPropertyOrder(14)]
        public string Closed { get; set; }

        /// <summary>
        /// Время завершения работы по сообщению
        /// </summary>
        [JsonPropertyName("close_time")]
        [JsonPropertyOrder(15)]
        public DateTime? CloseTime { get; set; }


        /// <summary>
        /// Текстовое описание статуса доставки сообщения
        /// </summary>
        [JsonPropertyName("status")]
        [JsonPropertyOrder(16)]
        public string Status { get; set; }


        /// <summary>
        /// Действие
        /// </summary>
        [JsonPropertyName("action")]
        [JsonPropertyOrder(17)]
        public string Action { get; set; }
    }
}
