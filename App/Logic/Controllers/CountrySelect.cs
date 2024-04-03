namespace HolidaysApp;

using CLI;
using HolidaysDatabase;

public static partial class AppLogic {
    public static async Task<bool> HandleCountrySelect(ChoiceList<Country> choiceList) {
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
                    await App.Holidays(choiceList.SelectedItem);
                }
                break;
            case ConsoleKey.Escape:
                return false;
        }

        return true;
    }
}