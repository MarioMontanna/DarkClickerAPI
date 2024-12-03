public class HttpRequestService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public HttpRequestService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Llamar al método del controlador directamente
                using (var scope = _serviceProvider.CreateScope())
                {
                    // Obtener el controlador del servicio
                    var controller = scope.ServiceProvider.GetRequiredService<ScoreController>();

                    // Llamar al método del controlador (que simula la lógica del GET)
                    var result = controller.TimeToResetDatabase();

                    // Imprimir la respuesta del resultado
                    Console.WriteLine($"Respuesta: {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al invocar el método: {ex.Message}");
            }

            // Esperar 15 minutos antes de realizar la siguiente llamada
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}
