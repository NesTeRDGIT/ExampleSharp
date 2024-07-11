using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using zms.Lib.Extensions;

namespace zms.Root.Api.FrontendApi.JwtBearerOption
{
    /// <summary>
    /// Фабрика JwtBearerEvents
    /// </summary>
    public class JwtBearerEventsFactory
    {
        /// <summary>
        /// Создать JwtBearerEvents
        /// </summary>
        /// <returns></returns>
        public static JwtBearerEvents CreateJwtBearerEvents(Serilog.ILogger logger)
        {
            return new JwtBearerEvents
            {
                //Ответ на доступ запрещен
                OnForbidden = context =>
                {
                    logger.Error("Ошибка авторизации: Доступ запрещен");
                    context.NoResult();
                    context.Response.Headers.AccessControlAllowOrigin = new StringValues("*");
                    var problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status403Forbidden,
                        Title = "Ошибка авторизации",
                        Detail = "Доступ запрещен"
                    };
                    context.Response.ContentType = "application/json; charset=utf-8";
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    var json = JsonSerializer.Serialize(problem);
                    context.Response.WriteAsync(json);
                    return Task.CompletedTask;
                },
                //Ответ на ошибки авторизации(Exception нужно в лог)
                OnAuthenticationFailed = context =>
                {
                    logger.Error(context.Exception, $"Ошибка авторизации; {GetRequestInfo(context.HttpContext)}");
                    context.NoResult();
                    context.Response.Headers.AccessControlAllowOrigin = new StringValues("*");
                    var problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Title = "Ошибка авторизации",
                        Detail = "Ошибка токена доступа"
                    };
                    context.Response.ContentType = "application/json; charset=utf-8";
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    var json = JsonSerializer.Serialize(problem);
                    context.Response.WriteAsync(json);
                    return Task.CompletedTask;
                },
                //Прочие ошибки, причем OnAuthenticationFailed прокидывает сюда тоже
                OnChallenge = context =>
                 {
                     context.HandleResponse();
                     if (context.Response.StatusCode.In(StatusCodes.Status401Unauthorized, StatusCodes.Status403Forbidden)) return Task.CompletedTask;
                     var errorMessage = $"{(string.IsNullOrEmpty(context.Error) && context.Request.Headers.Authorization.Count == 0 ? "Отсутствует токен" : context.Error)}";
                     logger.Error($"Ошибка авторизации: {errorMessage} : {GetRequestInfo(context.HttpContext)}");
                     context.Response.Headers.AccessControlAllowOrigin = new StringValues("*");
                     var problem = new ProblemDetails
                     {
                         Status = StatusCodes.Status401Unauthorized,
                         Title = "Ошибка авторизации",
                         Detail = errorMessage
                     };
                     context.Response.ContentType = "application/json; charset=utf-8";
                     context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                     var json = JsonSerializer.Serialize(problem);
                     context.Response.WriteAsync(json);
                     return Task.CompletedTask;
                 }
            };
        }

        private static string GetRequestInfo(HttpContext context)
        {
            var result = new List<string>
            {
                $"Path: '{context.Request.Path}'"
            };
            if (context.Request.Headers.TryGetValue("Authorization", out var token))
            {
                result.Add($"Authorization: '{token}'");
            }
            if (context.Connection.RemoteIpAddress != null)
            {
                result.Add($"RemoteIpAddress: '{context.Connection.RemoteIpAddress}:{context.Connection.RemotePort}'");
            }
            if (context.Request.Headers.TryGetValue("X-Forwarded-For", out var xForwardedFor))
            {
                result.Add($"X-Forwarded-For: '{xForwardedFor}'");
            }
            if (context.Request.Headers.TryGetValue("REMOTE_ADDR", out var remoteAddr))
            {
                result.Add($"REMOTE_ADDR: '{remoteAddr}'");
            }
            if (context.Request.Headers.TryGetValue("X-Real-IP", out var xRealIp))
            {
                result.Add($"X-Real-IP: '{xRealIp}'");
            }
            
            return string.Join(Environment.NewLine, result);
        }
    }
}
