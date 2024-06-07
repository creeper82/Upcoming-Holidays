namespace SharpViews;

/// <summary>
/// Holds a result of the dialog. Not a direct value like true/false, but properties you can access.
/// </summary>
/// <typeparam name="ReturnType">The return type of the dialog</typeparam>
public record DialogResult<ReturnType>
{
    /// <summary>
    /// Whether the user successfully interacted with the dialog. Values:
    /// <para><c>null</c> - The dialog is not finished (still displayed).</para>
    /// <para><c>false</c> - The dialog was dismissed (like a neutral option). No further action should be done.</para>
    /// <para><c>true</c> - The dialog was interacted with, and <c>DialogResult.ReturnedValue</c> contains the final value.</para>
    /// </summary>
    public bool? ActionTaken { get; set; } = null;

    /// <summary>
    /// The value returned by the dialog. If the dialog was dismissed or is still running, the value will be <c>default</c>
    /// </summary>
    public ReturnType? ReturnedValue { get; set; } = default;
}

/// <summary>
/// Represents a dialog screen. This differs from a normal screen by the possibility of returning a final value of chosen type.
/// </summary>
/// <typeparam name="ReturnType">The type of the returned value.</typeparam>
/// <param name="app">Interface of the app the dialog belongs to.</param>
public abstract class Dialog<ReturnType>(IParentApp app) : Screen(app)
{
    /// <summary>
    /// The result of the dialog, with more properties inside.
    /// </summary>
    internal DialogResult<ReturnType> DialogResult = new();

    /// <summary>
    /// Exits the dialog with <c>ActionTaken = false</c>. That means user dismissed the dialog. Like a neutral option. No further action
    /// should be done.
    /// </summary>
    protected void ExitDialogWithoutValue()
    {
        DialogResult.ActionTaken = false;
    }

    /// <summary>
    /// Exits the dialog with <c>ActionTaken = true</c> and given returned value.
    /// </summary>
    /// <param name="returnedValue">The value that the dialog will return.</param>
    protected void ExitDialogWithValue(ReturnType returnedValue)
    {
        DialogResult.ActionTaken = true;
        DialogResult.ReturnedValue = returnedValue;
    }
}

