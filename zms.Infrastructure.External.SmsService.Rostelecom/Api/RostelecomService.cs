using System.Net;
using System.Net.Http.Headers;
using System.Text;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Json;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.GetStatus;
using zms.Infrastructure.External.SmsService.Rostelecom.Api.Request.SendSms;

namespace zms.Infrastructure.External.SmsService.Rostelecom.Api
{
    /// <summary>
    /// Сервис отправки SMS от Rostelecom
    /// </summary>
    public class RostelecomService
    {
        private readonly RostelecomOptions options;
        private readonly RostelecomJsonSerializer rostelecomJsonSerializer;

        public RostelecomService(RostelecomOptions options, RostelecomJsonSerializer rostelecomJsonSerializer)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.rostelecomJsonSerializer = rostelecomJsonSerializer ?? throw new ArgumentNullException(nameof(rostelecomJsonSerializer));
        }

        /// <summary>
        /// Получить статус сообщений
        /// </summary>
        /// <returns></returns>
        public async Task<GetStatusResponse> GetStatusAsync(GetStatusRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            using var client = new HttpClient(handler);

           
            var requestUri = $"{options.Host}/api/v2/send_message/{request.SmsInfo.Id}";
            var message = new HttpRequestMessage(HttpMethod.Get, requestUri)
            {
                Headers = { Authorization = CreateAuthenticationHeaderValue() }
            };

            var result = await client.SendAsync(message);
            var body = await GetBodyAsync(result);
            return rostelecomJsonSerializer.Deserialize<GetStatusResponse>(body);
        }


        /// <summary>
        /// Получить статус сообщений
        /// </summary>
        /// <returns></returns>
        public async Task<SendSmsResponse> SendMessageAsync(SendSmsRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            using var client = new HttpClient(handler);

            var requestUri = $"{options.Host}/api/v2/send_message";
            var json = rostelecomJsonSerializer.Serialize(request);

            var message = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Method = HttpMethod.Post,
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
                Headers = { Authorization = CreateAuthenticationHeaderValue () }
            };

            var result = await client.SendAsync(message);
            var body = await GetBodyAsync(result);

            return rostelecomJsonSerializer.Deserialize<SendSmsResponse>(body);
        }

        private AuthenticationHeaderValue CreateAuthenticationHeaderValue()
        {
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{options.UserName}:{options.UserPassword}")));
        }


        private async Task<string> GetBodyAsync(HttpResponseMessage response)
        {
            var body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Exception innerException = null;
                if (!string.IsNullOrEmpty(body))
                {
                    innerException = new Exception(body);
                }
                throw new HttpRequestException($"{(int)response.StatusCode}: {response.ReasonPhrase}", innerException);
            }
            return body;
        }
    }
}
