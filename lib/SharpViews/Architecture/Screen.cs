namespace SharpViews;

/// <summary>
/// Represents a screen, like a single webpage or Android "Activity". The screen is meant to alternately: display the data, then handle the user input,
/// and the cycle repeats. The only way to stop that cycle is to either close the current screen
/// (calling <c>App.CloseScreen()</c> within <c>HandleInput</c>), or to
/// open a new screen (<c>App.OpenScreen(new ...)</c>)
/// </summary>
/// <param name="app">The interface to access parent app's properties and methods. You can't declare screens before declaring an app instance.</param>
public abstract class Screen(IParentApp app)
{
    /// <summary>
    /// The interface to access parent app's properties and methods.
    /// </summary>
    protected readonly IParentApp App = app;

    /// <summary>
    /// Gets the output handler of the current app. Used to write output, modify colors, and so on.
    /// </summary>
    protected IFullOutputHandler Output => App.Output;

    /// <summary>
    /// Gets the input handler of the current app. Used to get pressed keys, read input lines, etc.
    /// </summary>
    protected IInputHandler Input => App.Input;

    /// <summary>
    /// Gets the formatted text provider of the current app. Used to change text colors, font styling, etc.
    /// </summary>
    protected TextFormatting.IFormattedTextProvider F => App.FormattedTextProvider;

    /// <summary>
    /// Provides a set of utilites for dealing with error-prone method calls.
    /// </summary>
    protected IErrorUtils ErrorUtils => App.ErrorUtils;

    /// <summary>
    /// Gets the component provider of the current app. It contains easy-to-use text components such as frames, lists, centered text.
    /// The components return a string value, and are meant to be placed e.g. inside <c>Output.WriteLine()</c>
    /// </summary>
    protected ComponentProvider C => App.Components;

    /// <summary>
    /// Displays the <c>UiFrame</c> component with provided parameters. It is an alias to <c>Output.WriteLine(C.UiFrame(...))</c>. Useful for simple screens.
    /// </summary>
    /// <param name="inner">The content inside the frame.</param>
    /// <param name="title">The title that will be centered. Leave empty for none.</param>
    /// <param name="horizontalScroll">Whether to display horizontal scroll arrows.</param>
    /// <param name="verticalScroll">Whether to display vertical scroll arrows.</param>
    protected void DispUiFrame(string inner, string title = "", bool horizontalScroll = false, bool verticalScroll = false)
     => Output.WriteLine(C.UiFrame(inner: inner, title: title, horizontalScroll: horizontalScroll, verticalScroll: verticalScroll));

    /// <summary>
    /// Gets the window width in characters.
    /// </summary>
    protected int UiWidth => App.Output.UiWidth;

    /// <summary>
    /// Gets the window height in characters.
    /// </summary>
    protected int UiHeight => App.Output.UiHeight;

    /// <summary>
    /// Called when the screen is meant to display all the data. At this moment, the output is already cleared and ready for fresh data.
    /// Use the <c>Output</c> class and display data. Remember about all the cool components under the <c>C</c> class.
    /// </summary>
    public abstract void DisplayScreen();

    /// <summary>
    /// Called after the screen is displayed. At this moment, you might want to check for any pressed keys.
    /// </summary>
    public abstract Task HandleInput();

    /// <summary>
    /// A list of the keyboard actions, that will be displayed after the screen is rendered. Leave <c>null</c> for none.
    /// </summary>
    public abstract List<KeyboardAction>? KeyboardActions { get; }
}

/// <summary>
/// Represents an informational screen which doesn't contain any logic and keyboard actions. Pressing any key will exit the screen immediately.
/// </summary>
/// <param name="app"></param>
public abstract class AnyKeyExitScreen(IParentApp app) : Screen(app)
{
    /// <inheritdoc/>
    public override List<KeyboardAction>? KeyboardActions => null;

    /// <inheritdoc/>
    public override Task HandleInput()
    {
        Input.AnyKey();
        App.CloseScreen();
        return Task.CompletedTask;
    }
}