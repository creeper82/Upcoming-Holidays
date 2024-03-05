namespace CLI;

using static Components;
using HolidaysDatabase;

public partial class Screens
{
    public static void CountrySelect(List<Country> countries, int selectedIndex, int startIndex = 0)
    {
        ClearConsole();
        // Display menu
        Console.WriteLine(
            UiFrame(
                inner: CenteredText("Welcome. Select your country") +
                List(countries.Select(c => $"{c.EnglishName} ({c.IsoCode})")),

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