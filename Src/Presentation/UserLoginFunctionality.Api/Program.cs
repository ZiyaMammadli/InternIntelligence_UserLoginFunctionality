using AspNetCoreRateLimit;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using System.Threading.RateLimiting;
using UserLoginFunctionality.Application;
using UserLoginFunctionality.Application.ExceptionMiddleWares;
using UserLoginFunctionality.Infrastructure;
using UserLoginFunctionality.Persistence;

var builder = WebApplication.CreateBuilder(args);

//Load JSON configuration at the beginning
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();
// Add services to the container.

builder.Services.AddControllers(); 

builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "*",
            Limit = 5,  
            Period = "10s" 
        }
    };
});
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

//Swagger security settings
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InternIntelligence_UserLoginFunctionality", Version = "v1", Description = "InternIntelligence_UserLoginFunctionality swagger client." });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "You can type token after typing 'Bearer' and leaving a space \r\n\r\n For Instance : 'Bearer' sbfbsifbsiufbsiufbsuifb"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference =new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer",
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();


//Excption controller middelware
app.ExceptionHandlingMiddleWare();

// Rate limiting middleware
app.UseIpRateLimiting();

// Authentication ve Authorization middleware, order is important
app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; 
    });
}
else
{
    //Redirect HTTP requests to HTTPS
    app.UseHttpsRedirection();
    app.UseHsts();
}
//Should only work in Production
app.Use(async (context, next) =>
{
    if (!context.Request.IsHttps && app.Environment.IsProduction())
    {
        var httpsUrl = "https://" + context.Request.Host + context.Request.Path;
        context.Response.Redirect(httpsUrl, permanent: true);
        return;
    }
    await next();
});

app.MapControllers();

app.Run();
