using Ahlatci.Shop.Api.Filters;
using Ahlatci.Shop.Application.Automappings;
using Ahlatci.Shop.Application.Repositories;
using Ahlatci.Shop.Application.Services.Abstraction;
using Ahlatci.Shop.Application.Services.Implementation;
using Ahlatci.Shop.Application.Validators.Categories;
using Ahlatci.Shop.Domain.UWork;
using Ahlatci.Shop.Persistence.Context;
using Ahlatci.Shop.Persistence.Repositories;
using Ahlatci.Shop.Persistence.UWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Logging
var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

Log.Logger.Information("Program Started...");

// Add services to the container.

//ActionFilter registiration
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new ExceptionHandlerFilter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

//DbContext Registiration
builder.Services.AddDbContext<AhlatciContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AhlatciShop"));
});

//Repository Registiraction
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//UnitOfWork Registiration
builder.Services.AddScoped<IUnitWork, UnitWork>();

//Business Service Registiration
builder.Services.AddScoped<ICategoryService, CategoryService>();

//Automapper
builder.Services.AddAutoMapper(typeof(DomainToDto), typeof(ViewModelToDomain));

//FluentValidation Ýstekte gönderilen modele ait property'lerin istenen formatý destekleyip desteklemediðini anlamamýzý saðlar.
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateCategoryValidator));

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
