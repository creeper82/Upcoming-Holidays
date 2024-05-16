namespace UpcomingHolidays;

using SharpViews;

public static partial class AppLogic
{
    public enum HandleHolidaysResult {
        ContinueLoop, Cancel, MoveForward, MoveBackward, SwitchLanguage
    }

    public static HandleHolidaysResult HandleHolidays()
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        return consoleKey switch
        {
            ConsoleKey.S => HandleHolidaysResult.SwitchLanguage,
            ConsoleKey.Escape => HandleHolidaysResult.Cancel,
            ConsoleKey.DownArrow => HandleHolidaysResult.MoveForward,
            ConsoleKey.UpArrow => HandleHolidaysResult.MoveBackward,
            _ => HandleHolidaysResult.ContinueLoop,
        };
    }

}