namespace SharpViews;

/// <summary>
/// Extension methods for error handling related objects.
/// </summary>
public static class ErrorHandlingExtensions
{
    /// <summary>
    /// If the operation was successful, executes the given <c>action</c>, providing the result errorHandlingResult's value as the first parameter.
    /// </summary>
    /// <typeparam name="ReturnType">Return type of the </typeparam>
    /// <param name="errorHandlingResult">Object containing the error handling result</param>
    /// <param name="action">Called if the operation was successful, with the first parameter being the returned value.</param>
    /// <remarks>Even if there was an exception, but the user decided to retry and everything went ok, it is considered a successful operation.
    /// Unsuccessful operation is when user decided not to retry upon error.</remarks>
    public static void DoIfSuccess<ReturnType>(this ErrorHandlingResult<ReturnType> errorHandlingResult, Action<ReturnType> action)
    {
        if (errorHandlingResult.IsSuccess && errorHandlingResult.ReturnedValue is not null) action.Invoke(errorHandlingResult.ReturnedValue);
    }
}