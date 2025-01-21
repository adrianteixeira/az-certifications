using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo_03
{
    public class GetItemFunction
    {
        private readonly ILogger<GetItemFunction> _logger;

        public GetItemFunction(ILogger<GetItemFunction> logger)
        {
            _logger = logger;
        }

        [Function("GetItemFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request to get an item.");
            return new OkObjectResult("Item retrieved successfully!");
        }
    }
}
