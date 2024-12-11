public class HttpRequestService : BackgroundService
{
     private readonly ILogger<HttpRequestService> _logger;

    public HttpRequestService(ILogger<HttpRequestService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://darkclickerapi.onrender.com/api/Score/time-to-reset", stoppingToken);
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("Petición HTTP works.");
                    }
                    else
                    {
                        _logger.LogError($"Error in HTTP petition: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in the BackgroundService: {ex.Message}");
            }
            
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}
