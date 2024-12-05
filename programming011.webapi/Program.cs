using FluentValidation;
using FluentValidation.AspNetCore;
using NLog;
using NLog.Web;
using programming011.webapi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var logger = LogManager.Setup()
                       .LoadConfigurationFromFile("nlog.config")
                       .GetCurrentClassLogger();

try
{
    // Add services to the container.
    builder.Services.AddControllers();

    // Inject NLog logger for ILogger interface
    builder.Host.UseNLog();

    builder.Services.AddValidatorsFromAssemblyContaining<Program>()
                    .AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.MapControllers();

    app.Run();
}
catch(Exception exc)
{
    logger.Log(NLog.LogLevel.Error, exc);
}