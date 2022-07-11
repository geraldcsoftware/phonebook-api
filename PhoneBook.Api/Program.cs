using System.Reflection;
using FastEndpoints;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Data;
using PhoneBook.Api.Mapping;
using PhoneBook.Api.Validators;
using Serilog;
using Serilog.Templates;
using DTOs = PhoneBook.DTOs;

var builder = WebApplication.CreateBuilder(args);

const string logTemplate =
    "{@t:yyyy/MM/dd HH:mm:ss} [{@l} - {SourceContext}] {@m}\n{#if IsDefined(@x)}{@x}\n{#end}";

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
builder.Services.AddAuthorization();

builder.Services.AddDbContext<PhoneBookDbContext>((sp, options) =>
{
    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
    var connectionString = builder.Configuration.GetConnectionString("DbConnection");
    if (string.IsNullOrEmpty(connectionString))
        throw new Exception("Connection string not properly configured in the app configuration");
    options.UseNpgsql(connectionString);
    options.UseLoggerFactory(loggerFactory);
    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();
});

builder.Services.AddFluentValidation();
builder.Services.AddTransient<IValidator<DTOs.CreatePhoneBookRequest>, CreatePhoneBookRequestValidator>();
builder.Services.AddTransient<IValidator<DTOs.AddPhoneBookEntryRequest>, CreatePhoneBookEntryRequestValidator>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMapper();
builder.Services.AddFastEndpoints();

var app = builder.Build();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseFastEndpoints();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PhoneBookDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();