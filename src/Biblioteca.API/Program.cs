using Biblioteca.Application.Dtos;
using Biblioteca.Application.UseCases.Consultar;
using Biblioteca.Application.UseCases.Crear;
using Biblioteca.Domain;
using Biblioteca.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseInMemoryDatabase(builder.Configuration.GetConnectionString("MyDB")));

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));


builder.Services.AddScoped<IRequestHandler<GetlAllLibrosQuery, List<LibroDto>>, GetAllLibrosHandler>();
builder.Services.AddScoped<IRequestHandler<CreateLibroCommand, bool>, CreateLibroHandler>();


//();

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
