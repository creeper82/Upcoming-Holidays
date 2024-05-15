namespace UpcomingHolidays.CLI;

using static SharpViews.Components;
using HolidaysDatabase;

public partial class Screens
{
    public static void CountrySelect(IEnumerable<Country> countries, int selectedIndex, int startIndex = 0)
    {
        ClearConsole();
        // Display menu
        Console.WriteLine(
            UiFrame(
                inner: CenteredText("Welcome. Select your country") +
                List(countries.Select(c => $"{c.EnglishName} [{c.IsoCode}]"), selectedIndex, startIndex),

                title: "Holidays",
                verticalScroll: true
            )
        );

        // Display keyboard actions
        Console.WriteLine(
            KeyboardActionList(KeyboardActions.CountrySelectScreen)
        );
    }
}