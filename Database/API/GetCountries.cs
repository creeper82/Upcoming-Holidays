using System.Text.Json;

namespace HolidaysDatabase;

public class API {
    private static readonly HttpClient HttpClient = new();

    public static async Task<List<Country>> GetCountries() {
        var response = await HttpClient.GetAsync("https://openholidaysapi.org/Countries");

        if (response.IsSuccessStatusCode) {
            var json = await response.Content.ReadAsStringAsync();
            var serialized = JsonSerializer.Deserialize<List<Country>>(json);
            return serialized ?? [];
        }

        else throw new HttpRequestException(message: $"Failed to connect to database.", null, statusCode: response.StatusCode);
    }
}

