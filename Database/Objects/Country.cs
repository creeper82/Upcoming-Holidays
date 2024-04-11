using System.Text.Json.Serialization;

namespace HolidaysDatabase;

public class Country
{
    [JsonPropertyName("isoCode")]
    public required string IsoCode { get; set; }

    [JsonPropertyName("name")]
    public required CountryName[] CountryNames { get; set; }

    [JsonIgnore]
    public string EnglishName => CountryNames[0].Text;
}

public class CountryName
{
    [JsonPropertyName("language")]
    public required string Language { get; set; }

    [JsonPropertyName("text")]
    public required string Text { get; set; }
}