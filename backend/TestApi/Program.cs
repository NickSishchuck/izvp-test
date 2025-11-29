using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.OpenApi;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using TestApi.Implementations;
using TestApi.Implementations.Repositories;
using TestApi.Interfaces;
using TestApi.Middleware;
using TestApi.Swagger.Examples;
using TestApi.Validators;

namespace TestApi
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var сonfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(сonfig)
                .CreateLogger();

            try
            {
                Log.Information("Starting Test API");

                // Create WebApplication builder
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog();

                // Routing
                builder.Services.AddRouting(options =>
                {
                    options.LowercaseUrls = true;
                    options.LowercaseQueryStrings = true;
                });

                // Dependency Injection
                builder.Services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
                });

                builder.Services.AddAutoMapper(cfg =>
                {
                    cfg.AddMaps(typeof(Program).Assembly);
                });

                builder.Services.AddScoped<IJsonSerializer, JsonSerializer>();
                builder.Services.AddScoped<ITestEvaluationService, TestEvaluationService>();
                builder.Services.AddScoped<ITestRepository, TestRepository>();
                builder.Services.AddTransient<IAdminAuthService, AdminAuthServiceImplementation>();

                builder.Services.AddValidatorsFromAssemblyContaining<TestSubmitRequestValidator>();
                builder.Services.AddSwaggerExamplesFromAssemblyOf<TestSubmitRequestExample>();

                // Controllers
                builder.Services.AddControllers();

                // Swagger
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.ExampleFilters();
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Test API",
                        Version = "v1",
                        Description = "API for tests like Google Forms"
                    });
                });

                // CORS
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", policy =>
                    {
                        // Load allowed origins from configuration
                        var allowedOrigins = builder.Configuration
                            .GetSection("Cors:AllowedOrigins")
                            .Get<string[]>() ?? new[] { "http://localhost:5000" };

                        // Configure CORS policy
                        policy.WithOrigins(allowedOrigins)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
                });

                var app = builder.Build();

                // Middleware for global exception handling
                app.UseMiddleware<ExceptionHandlingMiddleware>();

                // Middleware for logging HTTP requests
                app.UseSerilogRequestLogging(options =>
                {
                    options.MessageTemplate =
                        "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000}ms";
                    options.EnrichDiagnosticContext = (diag, http) =>
                    {
                        diag.Set("RequestHost", http.Request.Host.Value);
                        diag.Set("ClientIP", http.Connection.RemoteIpAddress?.ToString());
                        diag.Set("UserAgent", http.Request.Headers["User-Agent"].FirstOrDefault());
                    };
                });

                // Swagger UI
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API v1");
                        c.RoutePrefix = string.Empty;
                    });
                }

                // app.UseHttpsRedirection();

                app.UseCors("AllowAll");

                app.UseAuthorization();

                app.MapControllers();

                // Kestrel server configuration
                var url = "http://0.0.0.0:5000";

                Log.Information("Starting Kestrel on {Url}", url);

                app.Run(url);

                Log.Information("Test API stopped gracefully");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
