namespace CLI;

using static Components;
using HolidaysDatabase;

public partial class Screens
{
    public static void Holidays(IEnumerable<Holiday> holidays, string countryName, string language)
    {
        ClearConsole();
        // Display menu
        Console.WriteLine(
            UiFrame(
                inner: List(holidays.Select(h => $"{h.HolidayNames[0].Text} [{language}]")),

                title: $"Holidays - {countryName}",
                verticalScroll: false
            )
        );

        // Display keyboard actions
        // Console.WriteLine(
        //     KeyboardActionList(KeyboardActions.CountrySelectScreen)
        // );
    }
}