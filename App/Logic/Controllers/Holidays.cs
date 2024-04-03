namespace HolidaysApp;

using CLI;

public static partial class AppLogic
{
    public class HandleHolidaysResult
    {
        public class SwitchLanguage : HandleHolidaysResult { }
        public class MoveForward : HandleHolidaysResult { }
        public class MoveBackward : HandleHolidaysResult { }
        public class ContinueLoop : HandleHolidaysResult { }
        public class Cancel : HandleHolidaysResult { }
    }
    public static HandleHolidaysResult HandleHolidays()
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        return consoleKey switch
        {
            ConsoleKey.S => new HandleHolidaysResult.SwitchLanguage(),
            ConsoleKey.Escape => new HandleHolidaysResult.Cancel(),
            ConsoleKey.DownArrow => new HandleHolidaysResult.MoveForward(),
            ConsoleKey.UpArrow => new HandleHolidaysResult.MoveBackward(),
            _ => new HandleHolidaysResult.ContinueLoop(),
        };
    }

}