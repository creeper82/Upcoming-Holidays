namespace UpcomingHolidays.CLI;
using SharpViews;

// List of commonly used keyboard actions among app screens
public static class KeyboardActions
{
    public static List<KeyboardAction> CountrySelectScreen { get; } = [
        new("up/down", "move selection"),
        new("enter", "select country"),
        KeyboardAction.LineSeparator,
        new("esc", "exit app"),
    ];

    public static List<KeyboardAction> ErrorScreen { get; } = [
        new("space", "retry"),
        new("esc", "cancel")
    ];

    public static List<KeyboardAction> HolidaysScreen { get; } = [
        new("s", "switch language (english <> native)"),
        new("up/down", "scroll list"),
        KeyboardAction.LineSeparator,
        new("esc", "go back")
    ];
}