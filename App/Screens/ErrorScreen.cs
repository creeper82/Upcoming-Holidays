namespace HolidaysApp;

using CLI;

public static partial class App
{
    /// <summary>
    /// Displays an error screen with options to retry or cancel.
    /// </summary>
    /// <param name="windowTitle">The title at the top of the error screen.</param>
    /// <param name="errorText">Text contents of the error that will be displayed.</param>
    /// <returns>A boolean determining whether the user wants to retry the action that caused an error.</returns>
    public static bool ErrorScreen(string windowTitle = "ERROR", string errorText = "Error was not recognized. Sorry")
    {
        bool running = true;

        while (running)
        {
            Screens.ErrorScreen(
                windowTitle: windowTitle,
                errorText: errorText
            );

            var handleResult = AppLogic.HandleErrorScreen();

            if (handleResult is AppLogic.HandleErrorScreenResult.Retry) return true;
            if (handleResult is AppLogic.HandleErrorScreenResult.Cancel) return false;

        }

        return false;

    }
}