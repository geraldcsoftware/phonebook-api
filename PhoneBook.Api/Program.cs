using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Commands;
using PhoneBook.Api.Data;
using PhoneBook.Api.Mapping;
using PhoneBook.Api.Validators;
using Serilog;
using Serilog.Templates;
using DTOs = PhoneBook.Api.DTOs;
using Commands = PhoneBook.Api.Commands;

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

builder.Services.AddFluentValidation();
builder.Services.AddTransient<IValidator<DTOs.CreatePhoneBookRequest>, CreatePhoneBookRequestValidator>();
builder.Services.AddTransient<IValidator<DTOs.AddEntryRequest>, CreatePhoneBookEntryRequestValidator>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMapper();

var app = builder.Build();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapGet("/api/phonebooks", async (string? searchText, IMediator mediator) =>
{
    var request = new GetPhoneBooksRequest { SearchText = searchText };
    var response = await mediator.Send(request);
    return Results.Ok(response);
});

app.MapPost("/api/phonebooks", async (DTOs.CreatePhoneBookRequest requestModel,
                                      IValidator<DTOs.CreatePhoneBookRequest> validator,
                                      IMediator mediator) =>
{
    var validationResult = validator.Validate(requestModel);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors);
    }

    var command = new Commands.CreatePhoneBookRequest(requestModel.Name!);
    var response = await mediator.Send(command);
    return Results.Ok(response);
});

app.MapGet("/api/phonebooks/{phoneBookId:guid}/entries",
           async (Guid phoneBookId, string? searchText, IMediator mediator) =>
           {
               var request = new Commands.GetPhoneEntriesRequest(phoneBookId, searchText!);
               var response = await mediator.Send(request);
               return Results.Ok(response);
           });

app.MapPost("/api/phonebooks/{phoneBookId:guid}/entries",
            async (Guid phoneBookId, DTOs.AddEntryRequest requestModel, 
                   IValidator<DTOs.AddEntryRequest> validator,
                   IMediator mediator) =>
            {
                var validationResult = validator.Validate(requestModel);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }
                
                var command = new Commands.CreatePhoneBookEntryRequest(phoneBookId,
                                                                       requestModel.Name!,
                                                                       requestModel.PhoneNumbers!);
                var response = await mediator.Send(command);
                return Results.Ok(response);
            });

app.Run();