namespace SharpViews;
using System.Text.Json;

/// <summary>
/// An error translator is used to convert an exception into a friendly error text. You must implement one yourself.
/// </summary>
public interface IExceptionTranslator
{
    /// <summary>
    /// Returns a friendly exception message that will be presented to the user, based on given exception.
    /// </summary>
    /// <param name="e">Exception to translate into user-friendly text.</param>
    /// <returns>User-friendly text about the exception.</returns>
    static abstract string GetErrorText(Exception e);
}

/// <summary>
/// The most basic error translator, which simply returns the content of the excpetion message, no matter what kind of exception happens.
/// </summary>
public class PlainExceptionTranslator : IExceptionTranslator
{
    /// <inheritdoc/>
    public static string GetErrorText(Exception e) => "An error occured. Message:\n" + e.Message;
}

/// <summary>
/// This general-use error translator covers common HttpClient exceptions, such as timeout, JSON parse failure, and no internet connection.
/// </summary>
public class HttpExceptionTranslator : IExceptionTranslator
{
    /// <inheritdoc/>
    public static string GetErrorText(Exception e)
    {
        if (e.InnerException is TimeoutException) return "The request has timed out. Check internet connection, or try again.";
        if (e is HttpRequestException e1) return "Error communicating with the server. Check internet connection.\nMessage: " + e1.Message;
        if (e is JsonException) return "Error while reading the data. Could not parse JSON";
        return "Unrecognized error occured. Sorry";
    }
}