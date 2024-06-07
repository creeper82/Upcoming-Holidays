namespace SharpViews;

/// <summary>
/// A class containing common dialogs to be used by <c>App.ShowDialog()</c>
/// </summary>
public static partial class Dialogs
{
    /// <summary>
    /// Creates a confirmation (Yes/No) dialog, with no "neutral" option.
    /// The dialog result's returned value will be <c>true</c>, or <c>false</c>.
    /// You may check the dialog result directly, like: <c>if (App.OpenDialog(new Dialogs.Confirm(...))) doSth();</c>. Such expression
    /// only works for confirm dialogs. Other dialogs require checking the <c>DialogResult</c>.
    /// </summary>
    /// <param name="app">Interface to access the app.</param>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message shown in the dialog.</param>
    /// <param name="okButton">Text in the ok button. Default: <c>OK</c></param>
    /// <param name="cancelButton">Text in the cancel button. Default: <c>Cancel</c></param>
    public class Confirm(
    IParentApp app,
    string title,
    string message,
    string okButton = "OK",
    string cancelButton = "Cancel"
) : Dialog<bool>(app)
    {
        /// <inheritdoc/>
        public override List<KeyboardAction>? KeyboardActions => [
            new("y", okButton),
            new("n", cancelButton)
        ];

        /// <inheritdoc/>
        public override void DisplayScreen()
        {
            Output.WriteLine(
                C.UiFrame(
                    title: title,
                    inner: C.CenteredWrappedText(message)
                )
            );
        }

        /// <inheritdoc/>
        public override Task HandleInput()
        {
            switch (Input.GetKey(true))
            {
                case ConsoleKey.Y:
                    ExitDialogWithValue(true); break;
                case ConsoleKey.N:
                    ExitDialogWithValue(false); break;
            }

            return Task.CompletedTask;
        }
    }
}