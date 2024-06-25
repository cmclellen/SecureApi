using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SecureApi.Api.Models;

namespace SecureApi.Api.Functions;

public static class GetPlaces
{
    [FunctionName(nameof(GetPlaces))]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
        HttpRequest req,
        ILogger log)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        };
        using var reader = new StreamReader("au.csv");
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<CityModel>().ToList();
        return new JsonResult(records);
    }
}