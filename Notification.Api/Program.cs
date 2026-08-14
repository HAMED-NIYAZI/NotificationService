using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication;
using Notification.Api.Authentication;
using Notification.Api.Di;
using Notification.Infrastructure;
using Notification.Persistence;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//added for loops
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddSignalR();


#region Register Services Repositories
builder.Services.RegisterPersistenceRepositories(builder.Configuration);
builder.Services.RegisterInfrastructureServices(builder.Configuration);
builder.Services.ConfigureApiServices();
#endregion




builder.Services
    .AddAuthentication("ApiKey")
    .AddScheme<
        AuthenticationSchemeOptions,
        ApiKeyAuthenticationHandler>(
            "ApiKey",
            _ => { });
builder.Services.AddAuthorization();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// Configure Swagger
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddMemoryCache();



#region HangFire
// Configure Hangfire to use MemoryStorage
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseMemoryStorage());

// Add the Hangfire server
builder.Services.AddHangfireServer(options =>
{
    options.WorkerCount = 5;
});
#endregion HangFire

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllers();



app.MapOpenApi();
app.MapScalarApiReference();


app.MapHub<NotificationHub>("/hubs/NotificationService");
app.UseHttpsRedirection();



app.Run();
