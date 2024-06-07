namespace SharpViews;

/// <summary>
/// The whole app instance, which will hold screens and manage them. To start the app, open a screen and then call <c>StartAsync</c>.
/// </summary>
public class SharpViewsApp : IParentApp
{
    /// <inheritdoc/>
    public IInputHandler Input { get; }

    /// <inheritdoc/>
    public IFullOutputHandler Output { get; }

    /// <inheritdoc/>
    public TextFormatting.IFormattedTextProvider FormattedTextProvider {get; }

    /// <inheritdoc/>
    public ComponentProvider Components { get; }

    /// <inheritdoc/>
    public IErrorUtils ErrorUtils { get; }

    /// <summary>
    /// A stack of the running app screens, where the top-most one is the currently visible one.
    /// </summary>
    private readonly Stack<Screen> Screens;

    /// <summary>
    /// The currently visible screen.
    /// </summary>
    private Screen CurrentScreen => Screens.Peek();

    /// <summary>
    /// Gets the window width in characters.
    /// </summary>
    public int UiWidth => Output.UiWidth;

    /// <summary>
    /// Gets the window height in characters.
    /// </summary>
    public int UiHeight => Output.UiHeight;
    
    /// <summary>
    /// Creates a new app instance, which will use given dependencies.
    /// </summary>
    /// <param name="inputHandler">The class responsible for getting user inputs. Most likely a new instance of <c>ConsoleInputHandler</c></param>
    /// <param name="outputHandler">The class responsible for outputting text. Most likely a new instance of <c>ConsoleOutputHandler</c></param>
    /// <param name="formattedTextProvider">The class responsible for displaying formatted text to the output.
    /// For console apps, consider <c>AnsiFormattedTextProvider</c></param>
    public SharpViewsApp(IInputHandler inputHandler, IFullOutputHandler outputHandler, TextFormatting.IFormattedTextProvider formattedTextProvider)
    {
        Input = inputHandler;
        Output = outputHandler;
        FormattedTextProvider = formattedTextProvider;
        Components = new(Output);
        ErrorUtils = new AppErrorUtils(this);
        Screens = new();
    }

    /// <inheritdoc/>
    public void CloseScreen() => Screens.Pop();

    /// <inheritdoc/>
    public void OpenScreen(Screen screen) => Screens.Push(screen);

    /// <inheritdoc/>
    public async Task<DialogResult<ReturnType>> OpenDialogAsync<ReturnType>(Dialog<ReturnType> dialog)
    {
        while (dialog.DialogResult.ActionTaken is null) await RedrawAndHandleInput(dialog);
        return dialog.DialogResult;
    }

    /// <inheritdoc/>
    public async Task<bool> OpenDialogAsync(Dialog<bool> dialog)
    {
        while (dialog.DialogResult.ActionTaken is null) await RedrawAndHandleInput(dialog);
        return dialog.DialogResult.ReturnedValue;
    }

    /// <summary>
    /// Starts the app loop, by displaying the added screen. If there are no screens, nothing will happen. You must first call <c>OpenScreen()</c>
    /// </summary>
    public async Task StartAsync()
    {
        while (Screens.Count != 0) await RedrawAndHandleInput(CurrentScreen);
    }

    /// <inheritdoc/>
    public void Exit() => Screens.Clear();

    private async Task RedrawAndHandleInput(Screen screen)
    {
        Output.Clear();
        screen.DisplayScreen();
        if (screen.KeyboardActions is not null) DisplayKeyboardActions(screen.KeyboardActions);
        await screen.HandleInput();
    }

    /// <summary>
    /// Displays the list of keyboard actions.
    /// </summary>
    /// <param name="keyboardActions">List of keyboard actions to display.</param>
    private void DisplayKeyboardActions(List<KeyboardAction> keyboardActions)
    {
        Output.WriteLine("\n" + Components.KeyboardActionList(keyboardActions));
    }
}
