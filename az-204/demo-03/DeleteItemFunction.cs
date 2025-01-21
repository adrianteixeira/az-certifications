using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo_03
{
    public class DeleteItemFunction
    {
        private readonly ILogger<DeleteItemFunction> _logger;

        public DeleteItemFunction(ILogger<DeleteItemFunction> logger)
        {
            _logger = logger;
        }

        [Function("DeleteItemFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request to delete an item.");
            return new OkObjectResult("Item deleted successfully!");
        }
    }
}
