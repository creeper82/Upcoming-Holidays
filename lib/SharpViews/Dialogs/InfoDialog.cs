namespace SharpViews;

public static partial class Dialogs
{
    /// <summary>
    /// Creates an informational dialog with only an option to close it.
    /// Similar to JavaScript's <c>alert()</c>, it only informs the user about something.
    /// No dialog result checking is needed afterwards. Just continue with the code.
    /// </summary>
    /// <param name="app">Interface to access the app.</param>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message shown in the dialog.</param>
    public class Info(
        IParentApp app,
        string title,
        string message
    ) : Dialog<bool>(app)
    {
        /// <inheritdoc/>
        public override List<KeyboardAction>? KeyboardActions => [new("enter", "ok")];

        /// <inheritdoc/>
        public override void DisplayScreen()
        {
            DispUiFrame(
                title: title,
                inner: C.CenteredWrappedText(message)
            );
        }
        /// <inheritdoc/>
        public override Task HandleInput()
        {
            if (Input.GetKey(true) == ConsoleKey.Enter) ExitDialogWithValue(true);
            return Task.CompletedTask;
        }
    }
}