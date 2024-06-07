namespace SharpViews;

/// <summary>
/// The interface used to access the app from within the screens. It exposes some of the methods and services, but not the whole app instance.
/// </summary>
public interface IParentApp
{
    /// <summary>
    /// An input interface, used to retrieve pressed keys, and clear the buffer.
    /// </summary>
    IInputHandler Input { get; }

    /// <summary>
    /// An output interface, used to display text, clear output, change colors, and get window dimensions.
    /// </summary>
    IFullOutputHandler Output { get; }

    /// <summary>
    /// An interface used to display formatted text in output.
    /// </summary>
    TextFormatting.IFormattedTextProvider FormattedTextProvider {get;}

    /// <summary>
    /// The component provider of the current app. It contains easy-to-use text components such as frames, lists, centered text.
    /// The components return a string value, and are meant to be placed e.g. inside <c>Output.WriteLine()</c>
    /// </summary>
    ComponentProvider Components { get; }

    /// <summary>
    /// Error utils make it easier to handle fragile operations which are likely to throw an exception. If an exception happens, an error dialog is shown,
    /// and the user may decide to retry the operation.
    /// </summary>
    IErrorUtils ErrorUtils { get; }
    
    /// <summary>
    /// Appends a new screen to the stack and displays it. The current screen will be shown again, when the user exits the new one.
    /// </summary>
    /// <param name="screen">The screen to open.</param>
    void OpenScreen(Screen screen);

    /// <summary>
    /// Opens a dialog, and waits until user takes action.
    /// </summary>
    /// <typeparam name="ReturnType">The type of the value returned by the dialog.</typeparam>
    /// <param name="dialog">The dialog to display.</param>
    /// <returns>A dialog result object, containing status and value.</returns>
    Task<DialogResult<ReturnType>> OpenDialogAsync<ReturnType>(Dialog<ReturnType> dialog);
    
    /// <summary>
    /// Opens a <b>confirmation</b> (true/false) dialog, and waits until user takes action.
    /// </summary>
    /// <param name="dialog">The dialog to display.</param>
    /// <returns><c>true</c> if the dialog was accepted, <c>false</c> if rejected.</returns>
    Task<bool> OpenDialogAsync(Dialog<bool> dialog);

    /// <summary>
    /// Closes the currently visible screen, and goes back to the previous one, if any.
    /// </summary>
    void CloseScreen();

    /// <summary>
    /// Exits the app completely, closing all the screens. To only close one screen, use <c>CloseScreen()</c>
    /// </summary>
    void Exit();
}