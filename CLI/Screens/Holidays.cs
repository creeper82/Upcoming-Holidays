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
                inner: List(holidays.Select(h => useEnglish ? h.EnglishName : h.NativeName)),

                title: $"Holidays - {countryName}",
                verticalScroll: false
            )
        );
        
        Console.WriteLine(
            KeyboardActionList(KeyboardActions.HolidaysScreen)
        );
    }
}