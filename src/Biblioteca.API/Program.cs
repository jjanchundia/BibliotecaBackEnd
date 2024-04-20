using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Biblioteca.Application.Dtos;
using Biblioteca.Persistence;
using MediatR;
using System.Reflection;
using Biblioteca.Application.UseCases.Libros.Consultar;
using Biblioteca.Application.UseCases.Libros.Eliminar;
using Biblioteca.Application.UseCases.Libros.Devolver;
using Biblioteca.Application.UseCases.Libros.Crear;
using Biblioteca.Application.UseCases.Libros.Prestar;
using Biblioteca.Application.UseCases.Usuarios.Crear;
using Microsoft.OpenApi.Models;
using Biblioteca.Application.UseCases.Usuarios.Login;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Agregamos politicas CORS para uso de endpoint localmente
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:SecretKey") ?? string.Empty));
// Configurar la autenticación con JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = key,
        };
    });

// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Biblioteca API", Version = "v1" });

    // Configuración de la autenticación en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        //Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        //Name = "Authorization",
        //In = ParameterLocation.Header,
        //Type = SecuritySchemeType.ApiKey
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                //new string[] { }
                Array.Empty<string>()
            }
        });
});

// Configurar la autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireLoggedIn", policy =>
    {
        policy.RequireAuthenticatedUser();
    });

    options.AddPolicy("RequireAdminRole", policy =>
    {
        policy.RequireRole("Admin");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de nuestra BD in Memory
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseInMemoryDatabase(builder.Configuration.GetConnectionString("MyDB")));

// Registro de servicio MediatR
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

//Inyectamos los servicios a nuestra clase program.cs
builder.Services.AddScoped<IRequestHandler<GetlAllLibrosQuery, List<LibroDto>>, GetAllLibrosHandler>();
builder.Services.AddScoped<IRequestHandler<CreateLibroCommand, bool>, CreateLibroHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteLibroCommand, bool>, DeleteLibroHandler>();
builder.Services.AddScoped<IRequestHandler<DevolverLibroCommand, bool>, DevolverLibroHandler>();
builder.Services.AddScoped<IRequestHandler<PrestarCommand, bool>, PrestarHandler>();
builder.Services.AddScoped<IRequestHandler<CreateUsuarioCommand, bool>, CreateUsuarioHandler>();
builder.Services.AddScoped<IRequestHandler<LoginCommand, bool>, LoginHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplica la política CORS a todas las solicitudes
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
