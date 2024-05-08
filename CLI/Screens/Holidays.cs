namespace CLI;

using static Components;
using HolidaysDatabase;

public partial class Screens
{
    public static void Holidays(IEnumerable<Holiday> holidays, string countryName, bool useEnglish = true)
    {
        ClearConsole();
        

        var content = (
            UiFrame(
                inner: HolidayList(holidays, useEnglish),

                title: $"Holidays - {countryName}",
                verticalScroll: true
            )
        );
        // Display content with color formatting
        new FormattedText(content).DisplayFormatted(useSpeed: false, newLine: true);

        Console.WriteLine(
            KeyboardActionList(KeyboardActions.HolidaysScreen)
        );

        if (UiHeight < 30) {
            new FormattedText("<yellow>Please increase the console height.</>").DisplayFormatted(useSpeed: false, newLine: true);
        }
    }
}