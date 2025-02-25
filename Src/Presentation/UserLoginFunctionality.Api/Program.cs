using UserLoginFunctionality.Application.ExceptionMiddleWares;
using UserLoginFunctionality.Infrastructure;
using UserLoginFunctionality.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);


builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json",optional:false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json",optional:true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

}
app.ExceptionHandlingMiddleWare();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
