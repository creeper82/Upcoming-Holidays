namespace SharpViews;

public static partial class Dialogs
{
    /// <summary>
    /// Creates a text input dialog, with possibility to dismiss it by entering an empty value.
    /// The dialog result's returned value will contain the entered oneline string. If the user decides to dismiss,
    /// <c>DialogResult.ActionTaken</c> will be <c>false</c>. Don't compare the returned value to <c>""</c> to check if dimissed.
    /// </summary>
    /// <param name="app">Interface to access the app.</param>
    /// <param name="title">Title of the dialog.</param>
    /// <param name="message">Message shown in the dialog.</param>
    /// <param name="required">Whether the user is required to type something (can't dismiss by leaving an empty string)</param>
    public class TextInput(
        IParentApp app,
        string title,
        string message,
        bool required = false
    ) : Dialog<string>(app)
    {
        /// <inheritdoc/>
        public override List<KeyboardAction>? KeyboardActions => null;

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
            Output.Write(">>> ");
            var input = Input.GetLine().Trim();
            if (input == "" && !required) ExitDialogWithoutValue();
            if (input != "" && !required) ExitDialogWithValue(input);

            return Task.CompletedTask;
        }
    }
}