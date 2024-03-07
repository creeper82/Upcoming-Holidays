using System.Text.Json;

namespace HolidaysDatabase;

public partial class API {
    private static readonly HttpClient HttpClient = new();

    public static async Task<List<Country>> GetCountries() {
        var response = await HttpClient.GetAsync("https://openholidaysapi.org/Countries");

        if (response.IsSuccessStatusCode) {
            var json = await response.Content.ReadAsStringAsync();
            var deserialized = JsonSerializer.Deserialize<List<Country>>(json);
            return deserialized ?? [];
        }

        else throw new HttpRequestException(message: $"Failed to connect to API.", null, statusCode: response.StatusCode);
    }
}

