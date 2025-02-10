// using System.Text.Json;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;

// namespace Pre_maritalCounSeling.BAL.EXE_WorkerService.EduSrc
// {
//     public class SyncDataService : BackgroundService
//     {
//         private readonly HttpClient _httpClient;
//         private readonly ILogger<SyncDataService> _logger;

//         private readonly IServiceScopeFactory _scopeFactory;
//         private readonly IConfiguration _configuration;

//         public SyncDataService(ILogger<SyncDataService> logger, IHttpClientFactory httpClientFactory, IServiceScopeFactory scopeFactory, IConfiguration configuration)
//         {
//             _logger = logger;
//             _httpClient = httpClientFactory.CreateClient();
//             _scopeFactory = scopeFactory;
//             _configuration = configuration;
//         }


//         protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             while (!stoppingToken.IsCancellationRequested)
//             {
//                 _logger.LogInformation("SyncData Worker Service running at " + DateTime.UtcNow);
//                 try
//                 {
//                     //call api from EduSource server
//                     var url = _configuration["EduSource:BaseAPI"];
//                     // var response = _httpClient.GetAsync("https://localhost:44300/api/v1/edu-source/sync-data").Result;

//                     foreach (var item in response)
//                     {

//                         //handle data

//                         using (var scope = _scopeFactory.CreateScope())
//                         {
//                             //init other service : repo, dbcontext, ...


//                             //save data to database

//                         }
//                     }
//                 }
//                 catch (Exception e)
//                 {
//                     //rollback data changes
//                     _logger.LogError(e, e.Message);
//                 }

//                 await Task.Delay(1000, stoppingToken); // Delay 1 second

//             }
//         }
//     }
// }