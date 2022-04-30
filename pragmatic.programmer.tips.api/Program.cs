using Microsoft.OpenApi.Models;
using pragmatic.programmer.tips.core;
using pragmatic.programmer.tips.core.data;
using pragmatic.programmer.tips.core.data.interfaces;
using pragmatic.programmer.tips.core.services;
using pragmatic.programmer.tips.core.services.interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// initialize logger
// var logger = new Logger(Logger.LogTo.Console, Logger.LogLevel.Debug);
var logger = new Logger(Logger.LogTo.File, "E:\\pragprogtips.api.logs", Logger.LogLevel.Debug);
builder.Services.AddSingleton(logger);

// initialize mailingListRepository and mailingListService
var mailingListRepository = new MailingListRepository();
var mailingListService = new MailingListService(mailingListRepository, logger);
builder.Services.AddSingleton<IMailingListRepository>(mailingListRepository);
builder.Services.AddSingleton<IMailingListService>(mailingListService);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Pragmatic Programmer Tips Email Service Mailing List API",
        Description = "api to subscribe/unsubscribe from the pragmatic.programmer.tips email service.",
        // TermsOfService = new Uri("https://example.com/terms"),
        // Contact = new OpenApiContact
        // {
        //     Name = "Example Contact",
        //     Url = new Uri("https://example.com/contact")
        // },
        // License = new OpenApiLicense
        // {
        //     Name = "Example License",
        //     Url = new Uri("https://example.com/license")
        // }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();