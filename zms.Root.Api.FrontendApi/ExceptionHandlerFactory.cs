using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zms.Common.SharedKernel.Exception;

namespace zms.Root.Api.FrontendApi
{
    /// <summary>
    /// Фабрика для обработчиков ошибки
    /// </summary>
    public class ExceptionHandlerFactory
    {
        /// <summary>
        /// Обработка исключения
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task ExceptionHandler(HttpContext context)
        {
            try
            {
                var problemDetails = new ProblemDetails
                {
                    Status = 500,
                    Title = "Внутренняя ошибка сервера"
                };
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionHandlerFeature != null)
                {
                    problemDetails =
                        exceptionHandlerFeature.Error switch
                        {
                            DomainException domainException => domainException switch
                            {
                                EntityNotExistDomainException notExistException => new ProblemDetails
                                {
                                    Detail = notExistException.Message,
                                    Status = 404,
                                    Title = "Ресурс не найден"
                                },

                                AggregateDomainException aggregateDomainException => CreateProblemDetails(aggregateDomainException),

                                not null => new ProblemDetails
                                {
                                    Detail = domainException.Message,
                                    Status = 400,
                                    Title = "Ошибка запроса"
                                }
                            },
                            DbUpdateConcurrencyException concurrencyException => new ProblemDetails
                            {
                                Detail = concurrencyException.Message,
                                Status = 409,
                                Title = "Невозможно изменить данные, т.к. они были изменены другим пользователем. Обновите данные или повторите попытку"
                            },
                            _ => new ProblemDetails
                            {
                                Detail = exceptionHandlerFeature.Error.Message,
                                Status = 500,
                                Title = "Внутренняя ошибка сервера"
                            }
                        };
                }

                context.Response.StatusCode = problemDetails.Status!.Value;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (Exception)
            {
                await context.Response.WriteAsJsonAsync(new ProblemDetails
                {
                    Status = 500,
                    Title = "Внутренняя ошибка сервера"
                });
            }

        }

        private static ProblemDetails CreateProblemDetails(AggregateDomainException aggregateDomainException)
        {
            var problemDetails = new ProblemDetails
            {
                Detail = aggregateDomainException.Message,
                Status = 400,
                Title = "Ошибка запроса"
            };

            problemDetails.Extensions.Add("errors", aggregateDomainException.Exceptions.Select(x => x.Message).ToArray());

            return problemDetails;
        }
    }
}
