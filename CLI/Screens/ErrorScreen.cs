namespace CLI;

using static Components;

public partial class Screens
{
    public static void ErrorScreen(string windowTitle = "ERROR", string errorText = "Error was not recognized. Sorry")
    {
        ClearConsole();
        // Display menu
        Console.WriteLine(
            UiFrame(
                inner: "An error occured.\n" +
                errorText,

                title: windowTitle,
                verticalScroll: false
            )
        );

        // Display keyboard actions
        Console.WriteLine(
            KeyboardActionList(KeyboardActions.ErrorScreen)
        );
    }
}