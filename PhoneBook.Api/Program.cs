using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Data;
using PhoneBook.Api.Mapping;
using Serilog;
using Serilog.Templates;

var builder = WebApplication.CreateBuilder(args);

const string logTemplate =
    "{@t:yyyy/MM/dd HH:mm:ss} [{@l} - {SourceContext}] {@m}{NewLine}{#if IsDefined(@x)}{@x}{NewLine}{#end}";

builder.Host.UseSerilog((context, config) =>
{
    var expressionTemplate = new ExpressionTemplate(logTemplate);
    config.ReadFrom.Configuration(context.Configuration, "Logging");
    config.WriteTo.Console(expressionTemplate);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
        {
            options.Authority = builder.Configuration["Authentication:Authority"];
            options.Audience = builder.Configuration["Authentication:Audience"];
        });

builder.Services.AddDbContext<PhoneBookDbContext>((sp, options) =>
{
    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
    var connectionString = builder.Configuration.GetConnectionString("DbConnection");
    if (string.IsNullOrEmpty(connectionString))
        throw new Exception("Connection string not properly configured in the app configuration");
    options.UseNpgsql(connectionString);
    options.UseLoggerFactory(loggerFactory);
});

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMapper();

var app = builder.Build();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapGet("/", () => "Hello World!");

app.Run();