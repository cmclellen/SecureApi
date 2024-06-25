using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using SecureApi.Api;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(builder =>
    {
        // Explicitly adding the extension middleware because
        // registering middleware when extension is loaded does not
        // place the middleware in the pipeline where required request
        // information is available.
        builder.UseFunctionsAuthorization();
    })
    .ConfigureServices((ctx, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services
            .AddFunctionsAuthentication(JwtBearerDefaults.AuthenticationScheme)
            // This is important as Bearer scheme is used by the runtime
            // and no longer supported by this framework.
            .AddJwtFunctionsBearer(options =>
            {
                options.Authority = "https://login.microsoftonline.com/dca5775e-99b4-497c-90c1-c8e73396999e/v2.0";
                options.Audience = "67226661-dd54-471e-a51e-36312accd09f";
            });
        services.AddTransient<IClaimsTransformation, EntraIdClaimsTransformation>();

        services.AddHttpContextAccessor();
        services.AddMicrosoftIdentityWebApiAuthentication(ctx.Configuration);
        services.Configure<LoggerFilterOptions>(options =>
        {
            // The Application Insights SDK adds a default logging filter that instructs ILogger to capture only Warning and more severe logs. Application Insights requires an explicit override.
            // Log levels can also be configured using appsettings.json. For more information, see https://learn.microsoft.com/en-us/azure/azure-monitor/app/worker-service#ilogger-logs
            var toRemove = options.Rules.FirstOrDefault(rule => rule.ProviderName
                                                                ==
                                                                "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");

            if (toRemove is not null)
            {
                options.Rules.Remove(toRemove);
            }
        });
    })
    .Build();

host.Run();