namespace CLI;

using static TextPositioning;

// Formatted text is an object to store a multi-color text
// It can be displayed by calling the DisplayFormatted() method

// see FormattedTextDecoder switch-case statement if you want to learn what kind of tags are usable.

/// <summary>
/// Represents a list of formatted text parts, which together create one text with various formattings, like colors.
/// Can be created from a list of these text parts, or decoded from a human-readable string.
/// </summary>
/// <param name="textParts">The list of <c>FormattedTextPart</c> objects that together create a whole text.</param>

public partial class FormattedText(List<FormattedTextPart> textParts)
{
    List<FormattedTextPart> TextParts { get; set; } = textParts;

    // Secondary constructor - you can create a formatted text from a string this:
    // "Hello world. My name is <green>John</>. Here are some colors: <yellow>yellow<blue>blue"

    /// <summary>
    /// Creates a formatted text instance based on the provided human-readable text.
    /// </summary>
    /// <param name="bracketFormattedText">
    /// The text to be formatted, with tags in brackets. Example: "{green}Hello{/} {yellow}world{/}"  (use angle brackets, like
    /// in HTML and XML!).
    /// </param>

    public FormattedText(string bracketFormattedText, bool centered = false) : this(FormattedTextDecoder.From(
        centered ? CenteredText(bracketFormattedText, addInstantTags: true) : bracketFormattedText
    )) { }

    public string PureText => string.Concat(TextParts.Select(part => part.Text));

    /// <summary>
    /// Displays the formatted text to console.
    /// </summary>
    /// <param name="newLine">Whether to place a new line after all the text. By default <c>false</c>.</param>
    /// <param name="useSpeed">Whether to account for speed while displaying text. By default <c>true</c></param>
    /// <param name="QuickDraw">Use to additionally speed up the drawing speed, for example for descriptions already seen before</param>
    public void DisplayFormatted(bool newLine = false, bool useSpeed = true, bool quickDraw = false)
    {
        foreach (var textPart in TextParts) textPart.WriteToConsole(useSpeed, speedMultiply: quickDraw ? 4f : 1);
        if (newLine) Console.WriteLine();
    }
}