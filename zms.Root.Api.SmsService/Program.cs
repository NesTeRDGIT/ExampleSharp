using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using zms.Infrastructure.Logging.Serilog;
using zms.Root.Api.SmsService.Json.Converters;
using zms.Root.Api.SmsService.Swagger;
using zms.Root.Module.Application;
using zms.Root.Module.SmsService;
using zms.Root.Module.SmsService.Beeline;

namespace zms.Root.Api.SmsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = SerilogFactory.Create(new LoggerConfiguration { IsConsole = true });

            try
            {
                logger.Information("Start SmsServiceApi");

                var builder = WebApplication.CreateBuilder(args);

                builder.Logging.ClearProviders();
                logger = SerilogFactory.Create(builder.Configuration); //замена консольного логгера на логгер по конфигурации

                logger.Information("SmsServiceApi building");

                AddServices(builder.Services, builder.Configuration, logger);
                var app = builder.Build();

                logger.Information("SmsServiceApi configuring");
                ConfigurePipeline(app);

                logger.Information("SmsServiceApi running");
                app.Run();
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Application cannot start");
            }
        }

        private static void AddServices(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.Converters.Add(new TimeSpanNullableJsonConverter());
            });

            services.AddApplication(configuration, options =>
            {
                options.UsingSerilogLogger = logger;
            });


            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options); //актуальна последн€€ верси€ API
                //options.ApiVersionSelector = new ConstantApiVersionSelector(new ApiVersion(2, 0)); //актуальна конкретна€ верси€ API
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
            });

            services.AddSmsService(configuration);
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("Content-Disposition");
                });
            });

            services.AddResponseCaching();
        }

        private static void ConfigurePipeline(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    foreach (var description in app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseHttpsRedirection();
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.MapControllers();
            //app.UseCors("CorsPolicy");
            //app.UseResponseCaching();
            //app.UseExceptionHandler(c => c.Run(ExceptionHandlerFactory.ExceptionHandler));
        }
    }
}