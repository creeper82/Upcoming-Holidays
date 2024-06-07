namespace SharpViews;

/// <summary>
/// An input handler implementation with <c>System.Console</c> methods.
/// </summary>
public class ConsoleInputHandler : IInputHandler
{
    /// <inheritdoc/>
    public void AnyKey()
    {
        Console.ReadKey();
    }

    /// <inheritdoc/>
    public void ClearBuffer()
    {
        while (Console.KeyAvailable) Console.ReadKey(true);
    }

    /// <inheritdoc/>
    public ConsoleKey GetKey(bool clearBuffer = false)
    {
        if (clearBuffer) ClearBuffer();
        return Console.ReadKey(true).Key;
    }

    /// <inheritdoc/>
    public string GetLine() => Console.ReadLine() ?? "";
}

/// <summary>
/// An output handler implementation with <c>System.Console</c> methods.
/// </summary>
public class ConsoleOutputHandler : IFullOutputHandler
{
    /// <inheritdoc/>
    public int UiWidth => Console.WindowWidth;

    /// <inheritdoc/>
    public int UiHeight => Console.WindowHeight;

    /// <inheritdoc/>
    public ConsoleColor ForegroundColor {
        get => Console.ForegroundColor;
        set => Console.ForegroundColor = value;
    }

    /// <inheritdoc/>
    public ConsoleColor BackgroundColor {
        get => Console.BackgroundColor;
        set => Console.BackgroundColor = value;
    }

    /// <inheritdoc/>
    public void Clear() => Console.Clear();

    /// <inheritdoc/>
    public void ResetColor() => Console.ResetColor();

    

    /// <inheritdoc/>
    public void Write(string text) => Console.Write(text);

    /// <inheritdoc/>
    public void WriteLine(string text) => Console.WriteLine(text);
}