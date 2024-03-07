namespace HolidaysApp;

using HolidaysDatabase;
using CLI;

public static partial class App
{
    public static async Task Holidays(Country selectedCountry)
    {
        Console.WriteLine("\nLoading...");

        bool running = true;
    
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        DateOnly dateFrom = today;
        DateOnly dateTo = new(today.Year + 1, 12, 31);

        var holidays = await API.GetHolidays(selectedCountry.IsoCode, dateFrom, dateTo);

        while (running)
        {
            Screens.Holidays(
                holidays,
                selectedCountry.EnglishName,
                "EN"
            );
            Console.ReadKey();
        }

    }
}