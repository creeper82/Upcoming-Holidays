namespace SharpViews;

/// <summary>
/// Holds information about the result of the <c>TryGetOrHandleError</c> function family operations. Indicates whether the operation was successful,
/// and what is the return value. 
/// </summary>
/// <typeparam name="ReturnType">The return value type of the function that was executed.</typeparam>
/// <param name="IsSuccess">Whether the operation was successful (even if retried after errors, but finally executed)</param>
/// <param name="ReturnedValue">The returned value by the function that was executed.</param>
/// <remarks>Non-get (<c>TryOrHandleError</c>) functions directly return a boolean, and not the whole <c>ErrorHandlingResult</c>.</remarks>
public record ErrorHandlingResult<ReturnType>(bool IsSuccess = false, ReturnType? ReturnedValue = default);
