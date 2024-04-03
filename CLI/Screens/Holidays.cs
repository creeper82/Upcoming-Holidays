namespace CLI;

using static Components;
using HolidaysDatabase;

public partial class Screens
{
    public static void Holidays(IEnumerable<Holiday> holidays, string countryName, bool useEnglish = true)
    {
        ClearConsole();
        // Display menu
        Console.WriteLine(
            UiFrame(
                inner: HolidayList(holidays, useEnglish),

                title: $"Holidays - {countryName}",
                verticalScroll: true
            )
        );
        
        Console.WriteLine(
            KeyboardActionList(KeyboardActions.HolidaysScreen)
        );
    }
}