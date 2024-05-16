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

        List<Holiday>? holidays = await TryGetOrHandleErrorAsync<HttpErrorHandler, List<Holiday>>(() =>
        {
            return API.GetHolidays(selectedCountry.IsoCode, dateFrom, dateTo);
        }, loadingText: true);

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

                if (handleResult is AppLogic.HandleHolidaysResult.SwitchLanguage) useEnglish = !useEnglish;
                else if (handleResult is AppLogic.HandleHolidaysResult.MoveForward) holidaysScrollList.MoveForward();
                else if (handleResult is AppLogic.HandleHolidaysResult.MoveBackward) holidaysScrollList.MoveBackward();
                else if (handleResult is AppLogic.HandleHolidaysResult.Cancel) running = false;
            }
        }
    }
}