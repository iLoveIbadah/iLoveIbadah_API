using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using System.Drawing.Text;
using iLoveIbadah.Persistence;
using iLoveIbadah.Application;
using iLoveIbadah.Infrastructure;
using iLoveIbadah.API.Middleware;
using iLoveIbadah.Diagnostic;
using iLoveIbadah.Identity;
using Serilog;
using Microsoft.Extensions.Http.Resilience;
//using Microsoft.Extensions.Http.Polly; // why doesn't it recognize this? i have the package Microsoft.Extensions.Http.Polly installed
using Polly; // is this Microsoft.Extensions.Http.Polly? i don't have polly installed but Microsoft.Extensions.Http.Polly and importing Microsoft.Extensions.Http.Polly doens't work but Polly does...
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
//AddSwaggerDoc(builder.Services); //using openapi documentation not swagger's one

// Add services to the container.

//------------------ BELOW is the code to get the connection string from Azure Key Vault for when deployed to Azure
var KeyVaultUrl = new Uri(builder.Configuration.GetSection("KeyVaultUrl").Value!);
var AzureCredential = new DefaultAzureCredential();
builder.Configuration.AddAzureKeyVault(KeyVaultUrl, AzureCredential);

AddSwaggerDoc(builder.Services);

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.CongfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi(opt =>
{
    opt.AddDocumentTransformer((document, context, CancellationToken) =>
    {
        document.Info.Title = "IbadahLover API";
        document.Info.Contact = new OpenApiContact()
        {
            Name = "Amir",
            Email = "programirbe@gmail.com"
        };

        return Task.CompletedTask;
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("InternalAppPolicy", // for i love ibadah cross platform apps
        builder => builder.AllowAnyOrigin()
            .WithMethods()
            .WithHeaders());

    options.AddPolicy("ExternalAppPolicy", // for external apps or scripts that need to access the API for community
        builder => builder.AllowAnyOrigin()
            .WithMethods()
            .WithHeaders());

    options.AddPolicy("InternalWebsitePolicy", // only my website can access some API enpoints, so even if they have the token they cannot access it
        builder => builder.WithOrigins("https://iloveibadah.app") // specify the allowed origin(s) here
            .AllowAnyHeader()
            .AllowAnyMethod());

    options.AddPolicy("ExternalWebsitePolicy", // for external apps or scripts that need to access the API for community
        builder => builder.AllowAnyOrigin()
            .WithMethods()
            .WithHeaders());

    options.AddPolicy("DevPolicy", // for development purposes only
        builder => builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddObservability("API", builder.Configuration, ["iLoveIbadah.API"]);

//builder.Services.AddHttpClient<>(httpClient =>
//{
//    httpClient.BaseAddress = new Uri("https://api.example.com/"); // Replace with your API base address
//}).AddStandardResilienceHandler(options =>
//{
//    options.RetryOp
//});

//builder.Services.AddHttpClient("ExternalAPI", client =>
//    {
//        client.BaseAddress = new Uri("https://") // External API base address!! 
//    })
//    .AddStandardResilienceHandler(options =>
//    {
//        options.Retry = new HttpRetryStrategyOptions
//        {
//            Delay = TimeSpan.FromSeconds(1),
//            MaxRetryAttempts = 2, // the default is 3, can customize it if you want like here
//        };

//        options.CircuitBreaker = new HttpCircuitBreakerStrategyOptions
//        {
//            //DurationOfBreak = TimeSpan.FromSeconds(30),
//            //FailureThreshold = 0.5f,
//            SamplingDuration = TimeSpan.FromSeconds(30),
//            MinimumThroughput = 10,
//            FailureRatio = 0.5,
//            BreakDuration = TimeSpan.FromSeconds(15)

//        };
//        options.AttemptTimeout = new HttpTimeoutStrategyOptions
//        {
//            Timeout = TimeSpan.FromSeconds(10)
//        };
//        options.RateLimiter = new HttpRateLimiterStrategyOptions
//        {

//        };

//        //options.CircuitBreaker
//        //options.Retry.Delay = TimeSpan.FromSeconds(1);
//        //options.Retry.MaxRetryAttempts = 2; // the default is 3, can customize it if you want like here
//    });

// implement Services classes for external api calls then do this TODO!:
//builder.Services.AddHttpClient<ProductService>(c =>
//{
//    var url = builder.Configuration["ProductEndpoint"] ?? throw new InvalidOperationException("ProductEndpoint is not set");

//    c.BaseAddress = new(url);
//}).AddStandardResilienceHandler();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IbadahLover API v1"));
    app.MapScalarApiReference();
    app.UseCors("DevPolicy"); // for development purposes only
}
else
{
    app.UseCors("InternalAppPolicy");
}

app.UseAuthentication();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
//app.UseAuthentication();
app.UseRouting(); // ??? just follow tutorial don't know what it does
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//app.MapControllers(); TO KNOW! if UseEndpoints from above has issue use this instead

app.MapObservability();

app.Run();

void AddSwaggerDoc(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. 
                            Enter 'Bearer' [space] and then your token in the text input below.
                            Example: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });

        c.SwaggerDoc("v1", new OpenApiInfo { Title = "IbadahLover API", Version = "v1" });
    });
}

//void AddSwaggerDoc(IServiceCollection services)
//{
//    services.AddSwaggerGen(c =>
//    {
//        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//        {
//            Description = @"JWT Authorization header using the Bearer scheme. 
//                            Enter 'Bearer' [space] and then your token in the text input below.
//                            Example: 'Bearer 12345abcdef'",
//            Name = "Authorization",
//            In = ParameterLocation.Header,
//            Type = SecuritySchemeType.ApiKey,
//            Scheme = "Bearer"
//        });

//        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
//        {
//            {
//                new OpenApiSecurityScheme
//                {
//                    Reference = new OpenApiReference
//                    {
//                        Type = ReferenceType.SecurityScheme,
//                        Id = "Bearer"
//                    },
//                    Scheme = "oauth2",
//                    Name = "Bearer",
//                    In = ParameterLocation.Header,
//                },
//                new List<string>()
//            }
//        });

//        c.SwaggerDoc("v1", new OpenApiInfo
//        {
//            Version = "v1",
//            Title = "IbadahLover API",
//        });
//    });
//}