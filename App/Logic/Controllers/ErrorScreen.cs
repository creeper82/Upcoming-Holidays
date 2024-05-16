namespace UpcomingHolidays;

using SharpViews;

public static partial class AppLogic
{
    public enum HandleErrorScreenResult {
        ContinueLoop, Retry, Cancel,
    }

    public static HandleErrorScreenResult HandleErrorScreen()
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        return consoleKey switch
        {
            ConsoleKey.Spacebar => HandleErrorScreenResult.Retry,
            ConsoleKey.Escape => HandleErrorScreenResult.Cancel,
            _ => HandleErrorScreenResult.ContinueLoop,
        };
    }
}