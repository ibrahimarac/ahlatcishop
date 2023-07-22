using Ahlatci.Shop.Api.Filters;
using Ahlatci.Shop.Application.Automappings;
using Ahlatci.Shop.Application.Services.Abstraction;
using Ahlatci.Shop.Application.Services.Implementation;
using Ahlatci.Shop.Persistence.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
//Business Service Registiration
builder.Services.AddScoped<ICategoryService, CategoryService>();

//Automapper
builder.Services.AddAutoMapper(typeof(DomainToDto), typeof(ViewModelToDomain));





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
