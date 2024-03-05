namespace HolidaysApp;

using HolidaysDatabase;
using CLI;

public static partial class App
{
    public static async Task CountrySelect()
    {
        Console.WriteLine("\nLoading...");
        ChoiceList<Country> countryChoiceList = new(await API.GetCountries())
        {
            PaginationCount = 5
        };

        countryChoiceList.choices = countryChoiceList.choices.OrderBy(c => c.EnglishName);

        bool running = true;

        while (running)
        {
            countryChoiceList.CheckOutOfBoundsPointer();
            Screens.CountrySelect(countryChoiceList.PaginatedChoices, countryChoiceList.selectedIndex, countryChoiceList.PaginationStartIndex);
            running = AppLogic.HandleCountrySelect(countryChoiceList);
        }

    }
}