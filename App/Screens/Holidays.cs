namespace HolidaysApp;

using HolidaysDatabase;
using CLI;

public static partial class App
{
    public static async Task Holidays(Country selectedCountry)
    {

        bool retryConnection = true;

        while (retryConnection)
        {
            Console.WriteLine("\nLoading...");

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            DateOnly dateFrom = today;
            DateOnly dateTo = new(today.Year + 1, 12, 31);

            List<Holiday> holidays = [];

            string? error = null;

            try
            {
                holidays = await API.GetHolidays(selectedCountry.IsoCode, dateFrom, dateTo);
            }

            catch (HttpRequestException e)
            {
                error = "Error communicating with server. Check internet connection.\nMessage: " + e.Message;
            }

            catch (Exception) { error = "Unrecognized error occured. Sorry"; }

            if (error is null)
            {
                retryConnection = false;
                bool running = true;
                bool useEnglish = false;

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
            } else {
                retryConnection = ErrorScreen("Holidays", error);
            }



        }


    }
}