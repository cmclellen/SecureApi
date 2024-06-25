using System;
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

public class GetPlaces(ILogger<GetPlaces> logger)
{
    [FunctionName(nameof(GetPlaces))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
        HttpRequest req, ExecutionContext context)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        };
        //var path = Path.Join(context.FunctionAppDirectory, "au.csv");
        var path = Path.Join(context.FunctionDirectory, "au.csv");
        logger.LogInformation("Current directory is {CurrentDirectory}.", path);
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<CityModel>().ToList();
        await Task.CompletedTask;
        return new JsonResult(records);
    }
}