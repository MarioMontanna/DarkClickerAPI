public class MonthlyDatabaseCleanUpService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public MonthlyDatabaseCleanUpService(IServiceProvider serviceProvider)
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

                        Console.WriteLine($"Date Base cleaned correctly at {now}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error cleaning database: {ex.Message}");
                    }
                }

                // wait a minute before checking the time again
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            // wait a minute before checking the time again
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
