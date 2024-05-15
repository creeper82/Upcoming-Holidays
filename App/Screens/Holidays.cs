namespace UpcomingHolidays;

using HolidaysDatabase;
using CLI;
using SharpViews;

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
                if (holidays.Count == 0) error = "Server responded with empty data.";
            }

            catch (Exception e) when (e.InnerException is TimeoutException)
            {
                error = "The request has timed out. Check internet connection, or try again if it is slow";
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

                ScrollableList<Holiday> holidaysScrollList = new(holidays)
                {
                    PaginationCount = 5
                };

                while (running)
                {
                    Screens.Holidays(
                        holidaysScrollList.PaginatedChoices,
                        selectedCountry.EnglishName,
                        useEnglish
                    );

                    var handleResult = AppLogic.HandleHolidays();

                    if (handleResult is AppLogic.HandleHolidaysResult.SwitchLanguage) useEnglish = !useEnglish;
                    else if (handleResult is AppLogic.HandleHolidaysResult.MoveForward) holidaysScrollList.MoveForward();
                    else if (handleResult is AppLogic.HandleHolidaysResult.MoveBackward) holidaysScrollList.MoveBackward();
                    else if (handleResult is AppLogic.HandleHolidaysResult.Cancel) running = false;
                }
            }
            else
            {
                retryConnection = ErrorScreen("Holidays", error);
            }



        }


    }
}