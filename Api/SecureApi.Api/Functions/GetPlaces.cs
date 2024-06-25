using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SecureApi.Api.Models;
using Microsoft.Azure.Functions.Worker;

namespace SecureApi.Api.Functions;

public class GetPlaces(ILogger<GetPlaces> logger)
{
    [Function(nameof(GetPlaces))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
        HttpRequest req)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        };
        //var path = Path.Join(context.FunctionAppDirectory, "au.csv");
        
        var path = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "au.csv");
        logger.LogInformation("Current directory is {CurrentDirectory}.", path);
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<CityModel>().ToList();
        await Task.CompletedTask;
        return new JsonResult(records);
    }
}