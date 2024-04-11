using System.Text.Json;

namespace HolidaysDatabase;

public partial class API
{
    // Main HttpClient used for all API requests
    private static readonly HttpClient HttpClient = new();

    public static async Task<List<Country>> GetCountries()
    {
        var response = await HttpClient.GetAsync("https://openholidaysapi.org/Countries");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            try
            {
                var deserialized = JsonSerializer.Deserialize<List<Country>>(json);
                return deserialized ?? [];
            }
            catch (JsonException)
            {
                return [];
            }

        }

        else throw new HttpRequestException(message: $"Failed to connect to API.", null, statusCode: response.StatusCode);
    }
}

