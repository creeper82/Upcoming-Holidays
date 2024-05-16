namespace UpcomingHolidays;

using HolidaysDatabase;
using CLI;
using SharpViews;
using static SharpViews.ErrorHandling;

public static partial class App
{
    public static async Task CountrySelect()
    {

        bool running = true;

        List<Country>? countries = await TryGetOrHandleErrorAsync<HttpErrorHandler, List<Country>>(API.GetCountries, loadingText: true);

        if (countries is not null)
        {
            ChoiceList<Country> countryChoiceList = new(countries.OrderBy(c => c.EnglishName)) {
                PaginationCount = 5
            };

            while (running)
            {
                Screens.CountrySelect(countryChoiceList.PaginatedChoices, countryChoiceList.SelectedIndex, countryChoiceList.PaginationStartIndex);
                running = await AppLogic.HandleCountrySelect(countryChoiceList);
            }
        }
    }
}