namespace zms.Root.Worker.SmsService
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = serviceProvider.CreateScope();
                var smsService = scope.ServiceProvider.GetRequiredService<SmsService>();
                var smsServiceOptions = scope.ServiceProvider.GetRequiredService<SmsServiceOptions>();
                await smsService.Run();
                await Task.Delay(smsServiceOptions.TimeOut, cancellationToken);
            }
        }
    }
}