using System.Text;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);  // Escucha en el puerto 80
});

// Agrega servicios al contenedor
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de la cadena de conexión
string connectionString = builder.Configuration.GetConnectionString("SQLiteConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Configuración de autenticación y autorización
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Registrar el servicio de limpieza mensual
builder.Services.AddHostedService<MonthlyDatabaseCleanupService>();

var app = builder.Build();

// Habilita Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();