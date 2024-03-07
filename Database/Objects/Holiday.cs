using System.Text.Json.Serialization;

namespace HolidaysDatabase;

public class Holiday {
    [JsonPropertyName("startDate")]
    public required DateOnly StartDate {get; set;}

    [JsonPropertyName("endDate")]
    public required DateOnly EndDate {get; set;}

    [JsonPropertyName("name")]
    public required HolidayName[] HolidayNames {get; set;}

    public string? NameForLang(string LanguageISO) => HolidayNames?.FirstOrDefault(n => n?.LanguageISO == LanguageISO, null)?.Text;
}

public class HolidayName {
    [JsonPropertyName("language")]
    public required string LanguageISO {get; set;}

    [JsonPropertyName("text")]
    public required string Text {get; set;}
}