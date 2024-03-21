using System.Text.Json;

namespace HolidaysDatabase;

public partial class API {

    public static async Task<List<Holiday>> GetHolidays(string countryISO, DateOnly dateFrom, DateOnly dateTo) {
        string dateFromStr = dateFrom.ToString("yyyy-MM-dd");
        string dateToStr = dateTo.ToString("yyyy-MM-dd");

        var response = await HttpClient.GetAsync(
        $"https://openholidaysapi.org/PublicHolidays?countryIsoCode={countryISO}&validFrom={dateFromStr}&validTo={dateToStr}"
        );

        if (response.IsSuccessStatusCode) {
            var json = await response.Content.ReadAsStringAsync();
            try {
                var deserialized = JsonSerializer.Deserialize<List<Holiday>>(json);
                return deserialized ?? [];
            }
            catch (JsonException) {
                return [];
            }
            
            
        }

        else throw new HttpRequestException(message: $"Failed to connect to API.", null, statusCode: response.StatusCode);
    }
}

