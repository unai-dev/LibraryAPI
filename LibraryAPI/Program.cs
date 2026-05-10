using LibraryAPI.Config;
using LibraryAPI.Interfaces;
using LibraryAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg =>
{
    // Short mapping (opcional)

}, typeof(Program));

builder.Services.AddDbContext<ApplicationDbContext>
    (options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuracion del servicio controlador
builder.Services.AddControllers();

// Configuracion de servicios adicionales
builder.Services.AddScoped<IAuthorService, AuthorService>();


var app = builder.Build();

// Endpoint de prueba -> ruta home
app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
