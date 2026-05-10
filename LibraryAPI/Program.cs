using LibraryAPI.Config;
using LibraryAPI.Interfaces;
using LibraryAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuracion automapper
builder.Services.AddAutoMapper(cfg =>
{
    // Short mapping (opcional)

}, typeof(Program));

// Configuracion conexion base de datows
builder.Services.AddDbContext<ApplicationDbContext>
    (options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuracion del servicio controlador
builder.Services.AddControllers();

// Configuracion de servicios adicionales
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

// Configuracion swagger service
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Montamos el middleware de swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoint de prueba -> ruta home
app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
