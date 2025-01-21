using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;

namespace demo_03
{
    public class SaveItemFunction
    {
        private readonly ILogger<SaveItemFunction> _logger;
        private readonly BlobServiceClient _blobServiceClient;

        public SaveItemFunction(ILogger<SaveItemFunction> logger, BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
        }

        [Function("SaveItemFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request to save an item.");

            var formCollection = await req.ReadFormAsync();
            if (formCollection.Files.Count == 0)
            {
                return new BadRequestObjectResult("No files uploaded.");
            }

            var file = formCollection.Files[0];
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient("my-container");
            var blobClient = blobContainerClient.GetBlobClient(file.FileName);

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return new OkObjectResult("File uploaded successfully!");
        }
    }
}
