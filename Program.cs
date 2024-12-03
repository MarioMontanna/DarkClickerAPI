using System.Text;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Only in dev
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);  // Listening in port 80
});

// Services added
builder.Services.AddControllers();

// Swagger config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration to conect
string connectionString = builder.Configuration.GetConnectionString("SQLiteConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Configuration de Authentication y Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Do petition every 5 minutes to keep the server in use
builder.Services.AddHostedService<HttpRequestService>();
// Monthly database reset
builder.Services.AddHostedService<MonthlyDatabaseCleanupService>();

var app = builder.Build();

// Swagger only dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();