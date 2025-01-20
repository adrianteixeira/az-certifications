using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace demo_01
{
    public class demo_01_http
    {
        private readonly ILogger<demo_01_http> _logger;

        public demo_01_http(ILogger<demo_01_http> logger)
        {
            _logger = logger;
        }

        [Function("validateCpf")]
        public async Task<IActionResult> ValidateCpf([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string cpf = req.Query["cpf"];

            if (string.IsNullOrEmpty(cpf))
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonSerializer.Deserialize<JsonElement>(requestBody);
                cpf = data.GetProperty("cpf").GetString();
            }

            if (string.IsNullOrEmpty(cpf))
            {
                return new BadRequestObjectResult("Please pass a valid CPF in the query string or in the request body.");
            }

            bool isValidCpf = ValidateCpf(cpf);

            string responseMessage = isValidCpf
                ? $"The CPF {cpf} is valid."
                : $"The CPF {cpf} is invalid.";

            return new OkObjectResult(responseMessage);
        }

        private bool ValidateCpf(string cpf)
        {
            if (cpf.Length != 11 || !long.TryParse(cpf, out _))
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
