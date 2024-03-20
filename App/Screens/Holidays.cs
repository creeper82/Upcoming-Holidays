namespace HolidaysApp;

using HolidaysDatabase;
using CLI;

public static partial class App
{
    public static async Task Holidays(Country selectedCountry)
    {
        Console.WriteLine("\nLoading...");

        bool running = true;
        bool useEnglish = false;

        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        DateOnly dateFrom = today;
        DateOnly dateTo = new(today.Year + 1, 12, 31);

        var holidays = await API.GetHolidays(selectedCountry.IsoCode, dateFrom, dateTo);

        while (running)
        {
            Screens.Holidays(
                holidays,
                selectedCountry.EnglishName,
                useEnglish
            );

            var handleResult = AppLogic.HandleHolidays();

            if (handleResult is AppLogic.HandleHolidaysResult.SwitchLanguage) useEnglish = !useEnglish;
            if (handleResult is AppLogic.HandleHolidaysResult.Cancel) running = false;
        }

    }
}