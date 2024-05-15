namespace UpcomingHolidays;

using SharpViews;

public static partial class AppLogic
{
    public class HandleErrorScreenResult
    {
        public class Retry : HandleErrorScreenResult { }

        public class Cancel : HandleErrorScreenResult { }

        public class ContinueLoop : HandleErrorScreenResult { }
    }

    public static HandleErrorScreenResult HandleErrorScreen()
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        return consoleKey switch
        {
            ConsoleKey.Spacebar => new HandleErrorScreenResult.Retry(),
            ConsoleKey.Escape => new HandleErrorScreenResult.Cancel(),
            _ => new HandleErrorScreenResult.ContinueLoop(),
        };
    }
}