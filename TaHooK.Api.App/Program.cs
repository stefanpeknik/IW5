using AutoMapper;
using FluentValidation.AspNetCore;
using Serilog;
using TaHooK.Api.BL.Installers;
using TaHooK.Common.Extensions;
using TaHooK.Api.DAL.Entities.Interfaces;
using TaHooK.Api.DAL.Extensions;
using TaHooK.Api.DAL.Installers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureDependencies(builder.Services, builder.Configuration);
ConfigureAutoMapper(builder.Services);


var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApiDocument();

var app = builder.Build();

ValidateAutoMapperConfiguration(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}


// Migrate database
using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<IDbMigrator>().Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureDependencies(IServiceCollection serviceCollection, IConfiguration configuration)
{
    var connectionString = configuration.GetConnectionString("SQLCONNSTR_DefaultConnection") ?? throw new ArgumentException("The connection string is missing");
    serviceCollection.AddInstaller<DALInstaller>(connectionString);
    serviceCollection.AddInstaller<BlInstaller>();
}

void ConfigureAutoMapper(IServiceCollection serviceCollection)
{
    serviceCollection.AddAutoMapper(typeof(IEntity), typeof(BlInstaller));
}

void ValidateAutoMapperConfiguration(IServiceProvider serviceProvider)
{
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}