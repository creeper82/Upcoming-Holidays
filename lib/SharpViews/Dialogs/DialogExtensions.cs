namespace SharpViews;

/// <summary>
/// Extension methods for the Dialog-related objects.
/// </summary>
public static class DialogExtensions {
    /// <summary>
    /// Executes one of the paths based on whether the dialog was accepted, or dismissed. If the dialog is still displayed, nothing happens.
    /// </summary>
    /// <typeparam name="ReturnType">Return type of the dialog.</typeparam>
    /// <param name="dialogResult">Dialog result to examine.</param>
    /// <param name="ifActionTaken">Called if the action was taken, with first parameter being the dialog's returned value.</param>
    /// <param name="ifDismissed">(optional) Called if the dialog was dismissed. Usually this shouldn't cause any further action.</param>
    public static void DoIf<ReturnType>(this DialogResult<ReturnType> dialogResult, Action<ReturnType> ifActionTaken, Action? ifDismissed = null) {
        DoIfActionTaken(dialogResult, ifActionTaken);
        DoIfDismissed(dialogResult, ifDismissed);
    }

    /// <summary>
    /// Executes the given <c>action</c>, if action was taken in the dialog. Dialog's returned value will be passed as first parameter.
    /// If the dialog was dismissed or is still displayed, nothing happens.
    /// </summary>
    /// <typeparam name="ReturnType">Return type of the dialog.</typeparam>
    /// <param name="dialogResult">Dialog result to examine.</param>
    /// <param name="action">Called if the action was taken, with the first parameter being the dialog's returned value.</param>
    public static void DoIfActionTaken<ReturnType>(this DialogResult<ReturnType> dialogResult, Action<ReturnType>? action) {
        if (dialogResult.ActionTaken == true && dialogResult.ReturnedValue is not null) action?.Invoke(dialogResult.ReturnedValue);
    }

    private static void DoIfDismissed<ReturnType>(this DialogResult<ReturnType> dialogResult, Action? action) {
        if (dialogResult.ActionTaken == false) action?.Invoke();
    }
}