namespace HolidaysApp;

using CLI;
using HolidaysDatabase;

public static partial class AppLogic {
    public static bool HandleCountrySelect(ChoiceList<Country> choiceList) {
        ConsoleKey consoleKey = ConsoleInput.GetConsoleKey();

        if (choiceList.SelectedItem is not null)

        switch (consoleKey) {
            case ConsoleKey.UpArrow:
                choiceList.MoveBackward();
                break;
            case ConsoleKey.DownArrow:
                choiceList.MoveForward();
                break;
            case ConsoleKey.Enter:
                if (choiceList.SelectedItem is not null) {
                    // in future this will open list of holidays for {SelectedItem.CountryIso}
                }
                break;
            case ConsoleKey.Escape:
                return false;

        }

        return true;
    }
}