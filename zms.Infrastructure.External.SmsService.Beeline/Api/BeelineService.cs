using System.Net;
using System.Text;
using zms.Infrastructure.External.SmsService.Beeline.Api.Json;
using zms.Infrastructure.External.SmsService.Beeline.Api.Request.GetStatus;
using zms.Infrastructure.External.SmsService.Beeline.Api.Request.SendSms;

namespace zms.Infrastructure.External.SmsService.Beeline.Api
{
    /// <summary>
    /// Сервис отправки SMS от Beeline
    /// https://a2p-sms.beeline.ru/support/manual
    /// </summary>
    public class BeelineService
    {
        private readonly BeelineOptions options;
        private readonly BeelineJsonSerializer beelineJsonSerializer;

        public BeelineService(BeelineOptions options, BeelineJsonSerializer beelineJsonSerializer)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.beelineJsonSerializer = beelineJsonSerializer ?? throw new ArgumentNullException(nameof(options));
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

            var json = beelineJsonSerializer.Serialize(request);

            var requestUri = $"{options.Host}/proto/http/rest";
            var message = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Method = HttpMethod.Post,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var result = await client.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return beelineJsonSerializer.Deserialize<GetStatusResponse>(await result.Content.ReadAsStringAsync()) ;
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

            var json = beelineJsonSerializer.Serialize(request);
            var requestUri = $"{options.Host}/proto/http/rest";
            var message = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Method = HttpMethod.Post,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            var result = await client.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return beelineJsonSerializer.Deserialize<SendSmsResponse>(await result.Content.ReadAsStringAsync());
        }
    }
}
