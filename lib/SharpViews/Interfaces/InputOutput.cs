namespace SharpViews;

/// <summary>
/// An input interface, used to retrieve pressed keys, and clear the buffer.
/// </summary>
public interface IInputHandler
{
    /// <summary>
    /// Gets the pressed key, and optionally clears the buffer.
    /// </summary>
    /// <param name="clearBuffer">Whether to clear the input buffer (keys that were pressed before GetKey).</param>
    /// <returns></returns>
    ConsoleKey GetKey(bool clearBuffer = false);

    /// <summary>
    /// Reads a whole line string from input, and returns it.
    /// </summary>
    string GetLine();

    /// <summary>
    /// Waits for any pressed key.
    /// </summary>
    void AnyKey();

    /// <summary>
    /// Clears the input buffer (keys that were pressed and not yet checked by e.g. <c>GetKey</c>)
    /// </summary>
    void ClearBuffer();
}

/// <summary>
/// An output interface used for writing to the output, changing and resetting the colors.
/// </summary>
public interface IColorOutputHandler
{
    /// <summary>
    /// Writes plain text to the output.
    /// </summary>
    /// <param name="text">Text to write.</param>
    void Write(string text);

    /// <summary>
    /// Writes plain text to the output, followed by a newline character.
    /// </summary>
    /// <param name="text">Text to write.</param>
    void WriteLine(string text);

    /// <summary>
    /// Gets or sets the foreground (text) color.
    /// </summary>
    ConsoleColor ForegroundColor { get; set; }

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    ConsoleColor BackgroundColor { get; set; }
    
    /// <summary>
    /// Resets the foreground and background color to default.
    /// </summary>
    void ResetColor();
}

/// <summary>
/// An output interface packed with all the features. Writing output, clearing, managing colors, and getting window dimensions.
/// It is an extended version of <c>IColorOutputHandler</c>
/// </summary>
public interface IFullOutputHandler : IColorOutputHandler, IDimensionsProvider
{
    /// <summary>
    /// Clears the output.
    /// </summary>
    void Clear();
}

