namespace UpcomingHolidays;

using HolidaysDatabase;
using SharpViews;

public static partial class AppLogic
{
    public static async Task<bool> HandleCountrySelect(ChoiceList<Country> choiceList)
    {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        if (choiceList.SelectedChoice is not null)

            switch (consoleKey)
            {
                case ConsoleKey.UpArrow:
                    choiceList.MoveBackward();
                    break;
                case ConsoleKey.DownArrow:
                    choiceList.MoveForward();
                    break;
                case ConsoleKey.Enter:
                    if (choiceList.SelectedChoice is not null)
                    {
                        await App.Holidays(choiceList.SelectedChoice);
                    }
                    break;
                case ConsoleKey.Escape:
                    return false;
            }
        return true;
    }
}