namespace SharpViews;

/// <summary>
/// Utilities used to handle exceptions in a quicker, and simpler way.
/// </summary>
public interface IErrorUtils
{
    Task<bool> TryOrHandleError<ExceptionTranslator>(Action action) where ExceptionTranslator : IExceptionTranslator;
    Task<bool> TryOrHandleErrorAsync<ExceptionTranslator>(Func<Task> function) where ExceptionTranslator : IExceptionTranslator;
    Task<ErrorHandlingResult<ReturnType>> TryGetOrHandleError<ExceptionTranslator, ReturnType>(Func<ReturnType> function) where ExceptionTranslator : IExceptionTranslator;
    Task<ErrorHandlingResult<ReturnType>> TryGetOrHandleErrorAsync<ExceptionTranslator, ReturnType>(Func<Task<ReturnType>> function) where ExceptionTranslator : IExceptionTranslator;
}