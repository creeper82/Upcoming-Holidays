namespace SharpViews;

/// <summary>
/// Utilities used to handle exceptions in a quicker, and simpler way.
/// </summary>
/// <param name="App">App instance where the error dialogs will be displayed.</param>
public class AppErrorUtils(IParentApp App) : IErrorUtils
{
    /// <summary>
    /// Tries to execute the given function and retrieve the returned value from it. If an exceptions occurs, displays it in a dialog screen,
    /// with possibility to retry or abandon it.
    /// User is allowed to retry the operation infinite times. If at some point it works without exception, the operation is considered a success.
    /// </summary>
    /// <typeparam name="ExceptionTranslator">Exception translator. Most basic one is <c>PlainExceptionTranslator</c></typeparam>
    /// <typeparam name="ReturnType">Type that the given function returns.</typeparam>
    /// <param name="function">Function to try to retrieve the return value from.</param>
    /// <returns>An object containing both operation status and returned value. Use helpers, like <c>.DoIfSuccess()</c> for best experience.</returns>
    public async Task<ErrorHandlingResult<ReturnType>> TryGetOrHandleError<ExceptionTranslator, ReturnType>(Func<ReturnType> function) where ExceptionTranslator : IExceptionTranslator
    {
        while (true)
        {
            try
            {
                return new(true, function.Invoke());
            }
            catch (Exception e)
            {
                if (!await App.OpenDialogAsync(ErrorDialog(errorMsg: ExceptionTranslator.GetErrorText(e)))) return new(false, default);
            }
        }
    }

    /// <summary>
    /// Tries to execute the given function and retrieve the returned value from it. If an exceptions occurs, displays it in a dialog screen,
    /// with possibility to retry or abandon it.
    /// User is allowed to retry the operation infinite times. If at some point it works without exception, the operation is considered a success.
    /// </summary>
    /// <typeparam name="ExceptionTranslator">Exception translator. Most basic one is <c>PlainExceptionTranslator</c></typeparam>
    /// <typeparam name="ReturnType">Type that the given function returns.</typeparam>
    /// <param name="function"><b>Async</b> function to try to retrieve the return value from.</param>
    /// <returns>An object containing both operation status and returned value. Use helpers, like <c>.DoIfSuccess()</c> for best experience.</returns>
    /// <remarks>For non-async functions, see <c>TryGetOrHandleError</c>. Async refers to the function you pass as an argument.
    /// The <c>TryOr</c> family of functions is always async itself, and must be awaited due to the nature of app dialogs.</remarks>
    public async Task<ErrorHandlingResult<ReturnType>> TryGetOrHandleErrorAsync<ExceptionTranslator, ReturnType>(Func<Task<ReturnType>> function) where ExceptionTranslator : IExceptionTranslator
    {
        while (true)
        {
            try
            {
                return new(true, await function.Invoke());
            }
            catch (Exception e)
            {
                if (!await App.OpenDialogAsync(ErrorDialog(errorMsg: ExceptionTranslator.GetErrorText(e)))) return new(false, default);
            }
        }
    }

    /// <summary>
    /// Tries to execute the given action. If an exceptions occurs, displays it in a dialog screen,
    /// with possibility to retry or abandon it.
    /// User is allowed to retry the operation infinite times. If at some point it works without exception, the operation is considered a success.
    /// </summary>
    /// <typeparam name="ExceptionTranslator">Exception translator. Most basic one is <c>PlainExceptionTranslator</c></typeparam>
    /// <param name="action">Action to try to execute.</param>
    /// <returns>Whether the operation was successful. <c>true/false</c></returns>
    public async Task<bool> TryOrHandleError<ExceptionTranslator>(Action action) where ExceptionTranslator : IExceptionTranslator
    {
        while (true)
        {
            try
            {
                action.Invoke(); return true;
            }
            catch (Exception e)
            {
                if (!await App.OpenDialogAsync(ErrorDialog(errorMsg: ExceptionTranslator.GetErrorText(e)))) return false;
            }
        }
    }

    /// <summary>
    /// Tries to execute the given <b>async</b> function (Task). If an exceptions occurs, displays it in a dialog screen,
    /// with possibility to retry or abandon it.
    /// User is allowed to retry the operation infinite times. If at some point it works without exception, the operation is considered a success.
    /// </summary>
    /// <typeparam name="ExceptionTranslator">Exception translator. Most basic one is <c>PlainExceptionTranslator</c></typeparam>
    /// <param name="function"><b>Async</b> function to try to execute</param>
    /// <returns>An object containing both operation status and returned value. Use helpers, like <c>.DoIfSuccess()</c> for best experience.</returns>
    /// <remarks>For non-async functions, see <c>TryOrHandleError</c>. Async refers to the function you pass as an argument.
    /// The <c>TryOr</c> family of functions is always async itself, and must be awaited due to the nature of app dialogs.
    /// <para>If your desired function is supposed to return something, change <c>Try</c> to <c>TryGet</c>. </para>
    /// </remarks>
    public async Task<bool> TryOrHandleErrorAsync<ExceptionTranslator>(Func<Task> function) where ExceptionTranslator : IExceptionTranslator
    {
        while (true)
        {
            try
            {
                await function.Invoke(); return true;
            }
            catch (Exception e)
            {
                if (!await App.OpenDialogAsync(ErrorDialog(errorMsg: ExceptionTranslator.GetErrorText(e)))) return false;
            }
        }
    }

    private Dialogs.Confirm ErrorDialog(string errorMsg) => new(
        app: App,
        title: "Error",
        message: "An error occured:\n" + errorMsg,
        okButton: "retry",
        cancelButton: "cancel"
    );
}