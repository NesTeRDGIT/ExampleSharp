using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Net.Http;
using Microsoft.Extensions.FileProviders;
using zms.Infrastructure.Logging.Serilog;
using zms.Infrastructure.Utility.LightRead.WebApi;
using zms.Root.Api.FrontendApi.JwtBearerOption;
using zms.Root.Api.FrontendApi.Swagger;
using zms.Root.Module.SmsService;
using zms.Root.Module.Thesaurus;
using zms.UI.ApiControllers.ExpertAccounting.V1.Model.Json;

namespace zms.Root.Api.FrontendApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = SerilogFactory.Create(new LoggerConfiguration { IsConsole = true });

            try
            {
                logger.Information("Start FrontendApi");

                var builder = WebApplication.CreateBuilder(args);

                //добавление параметров конфигурации через отдельные файлы (поддержка секретов Docker)
                if (builder.Configuration["ConfigurationFilesPath"] != null)
                    builder.Configuration.AddKeyPerFile(directoryPath: builder.Configuration["ConfigurationFilesPath"], optional: true);
                
                builder.Logging.ClearProviders();
                logger = SerilogFactory.Create(builder.Configuration); //замена консольного логгера на логгер по конфигурации

                logger.Information("FrontendApi building");

                AddServices(builder.Services, builder.Configuration, logger);
                ConfigureAuthorization(builder.Services, builder.Configuration, logger);
                var app = builder.Build();
                logger.Information("FrontendApi configuring");
                ConfigurePipeline(app);

                logger.Information("FrontendApi running");
                app.Run();
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Application cannot start");
            }
        }

        private static void AddServices(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
        {
            services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new LightReadBinderProvider());
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            services.AddApplication(configuration, options =>
            {
                options.UsingSerilogLogger = logger;
            });

            services.AddSmsService(configuration);
            services.AddEmailService(configuration);
            services.AddEmployment(configuration, options => options.UseAcl());
            services.AddArticles(configuration);

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.Converters.Add(new ThesaurusJsonConverterFactory());
            });

            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new MediaTypeApiVersionReader("api-version");
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options); //актуальна последн€€ верси€ API
                //options.ApiVersionSelector = new ConstantApiVersionSelector(new ApiVersion(2, 0)); //актуальна конкретна€ верси€ API
                options.AssumeDefaultVersionWhenUnspecified = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("Content-Disposition", "Access-Control-Allow-Origin");
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors("CorsPolicy");
            app.UseResponseCaching();
            app.UseExceptionHandler(c => c.Run(ExceptionHandlerFactory.ExceptionHandler));
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }

        private static void ConfigureAuthorization(IServiceCollection services, IConfiguration configuration, Serilog.ILogger logger)
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            var identityServerHost = configuration["IdentityServer:Host"];
            var trustIdentityServerValue = configuration["IdentityServer:Trust"];
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", opt =>
                {
                    opt.RequireHttpsMetadata = true;

                    opt.Authority = $"https://{identityServerHost}";
                    if (bool.TryParse(trustIdentityServerValue, out var trustIdentityServer))
                    {
                        if (!trustIdentityServer)
                            opt.BackchannelHttpHandler = new HttpClientHandler
                            {
                                ServerCertificateCustomValidationCallback = delegate { return true; }
                            };
                    }
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        RequireExpirationTime = true,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                    opt.Events = JwtBearerEventsFactory.CreateJwtBearerEvents(logger);
                    opt.IncludeErrorDetails = true;
                });
        }
    }
}