using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

public class MonthlyDatabaseCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public MonthlyDatabaseCleanupService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;

            if (now.Day == 1 && now.Hour == 0 && now.Minute == 0)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    try
                    {
                        context.MaxScores.RemoveRange(context.MaxScores);
                        await context.SaveChangesAsync();

                        Console.WriteLine($"Base de datos vaciada correctamente a las {now}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al vaciar la base de datos: {ex.Message}");
                    }
                }

                // Espera un minuto para evitar ejecuciones repetidas en la misma hora
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            // Espera un minuto antes de comprobar la hora de nuevo
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
