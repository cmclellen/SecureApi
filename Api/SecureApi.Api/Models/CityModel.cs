using CsvHelper.Configuration.Attributes;

namespace SecureApi.Api.Models;

public class CityModel
{
    [Name("city")] public string City { get; set; }

    [Name("lat")] public double Latitude { get; set; }

    [Name("lng")] public double Longitude { get; set; }

    [Name("country")] public string Country { get; set; }

    [Name("iso2")] public string Iso2 { get; set; }

    [Name("admin_name")] public string State { get; set; }

    [Name("population")] public int Population { get; set; }

    [Name("population_proper")] public int PopulationProper { get; set; }
}