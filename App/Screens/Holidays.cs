namespace UpcomingHolidays;

using HolidaysDatabase;
using CLI;
using SharpViews;
using static SharpViews.ErrorHandling;

public static partial class App
{
    public static async Task Holidays(Country selectedCountry)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Now);
        DateOnly dateFrom = today;
        DateOnly dateTo = new(today.Year + 1, 12, 31);

        var holidays = await TryGetOrHandleErrorAsync<HttpErrorHandler, List<Holiday>>(
            () => API.GetHolidays(selectedCountry.IsoCode, dateFrom, dateTo),
            loadingText: true
        );

        if (holidays is not null)
        {
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

                if (handleResult == AppLogic.HandleHolidaysResult.SwitchLanguage) useEnglish = !useEnglish;
                else if (handleResult == AppLogic.HandleHolidaysResult.MoveForward) holidaysScrollList.MoveForward();
                else if (handleResult == AppLogic.HandleHolidaysResult.MoveBackward) holidaysScrollList.MoveBackward();
                else if (handleResult == AppLogic.HandleHolidaysResult.Cancel) running = false;
            }
        }
    }
}