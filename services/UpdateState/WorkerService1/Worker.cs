using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient client;
        private readonly IConfiguration Configuration;
   

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var stringContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("magazineName", "value1"),
                    new KeyValuePair<string, string>("productOne", "value2"),
                    new KeyValuePair<string, string>("productTwo", "value3"),
                });
                var result = await client.PutAsync("http://localhost:5000/api/stock", stringContent);

                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Website Status code: {StatusCode}", result.StatusCode);
                    _logger.LogInformation("Data: {res}", result);
                    
                }
                else
                {
                    _logger.LogError("The website is not up. Status code: {StatusCode}", result.StatusCode);
                }
                await Task.Delay(Convert.ToInt32(Configuration["DelayTimestamp"]) * 1000, stoppingToken);
            }
        }
    }
}
