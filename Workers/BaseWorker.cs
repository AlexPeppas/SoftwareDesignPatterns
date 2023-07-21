namespace Workers
{
    public class BaseWorker : BackgroundService
    {
        private readonly ILogger<BaseWorker> logger;

        private HttpClient httpClient;

        public BaseWorker(ILogger<BaseWorker> logger)
        {
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await this.httpClient.GetAsync("https://www.iamtimcorey.com");

                if (result.IsSuccessStatusCode)
                {
                    this.logger.LogInformation("success");

                    await Task.Delay(5000, stoppingToken);

                    continue;
                }

                this.logger.LogWarning("error");

                await Task.Delay(5000, stoppingToken);

            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            this.httpClient = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            this.httpClient.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}