using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using DarkLoop.Azure.Functions.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SecureApi.Api.Models;

namespace SecureApi.Api.Functions;

[FunctionAuthorize]
public class GetPlaces(
    ILogger<GetPlaces> logger)
{
    [Function(nameof(GetPlaces))]
    [Authorize]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
        HttpRequest req)
    {
        var path = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "au.csv");

        var user = req.HttpContext.User.FindFirst("name")!.Value;

        logger.LogInformation("Current directory is {CurrentDirectory} {UserName}.", path, user);

        var records = await GetCityModelsAsync(path);

        return new JsonResult(records);
    }

    private async Task<List<CityModel>> GetCityModelsAsync(string path)
    {
        using var reader = new StreamReader(path);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        };
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<CityModel>().ToList();
        await Task.CompletedTask;
        return records;
    }
}