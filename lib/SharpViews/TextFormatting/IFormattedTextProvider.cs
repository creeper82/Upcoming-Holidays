#pragma warning disable CS1591

namespace SharpViews;

/// <summary>
/// All the text formatting utilities.
/// </summary>
public static partial class TextFormatting
{
    /// <summary>
    /// An interface used to display formatted text into the output.
    /// </summary>
    public interface IFormattedTextProvider
    {
        /// <summary>
        /// Resets all the applied formatting. Both colors and font styling.
        /// </summary>
        string Reset { get; }

        /// <summary>
        /// Makes the text bold (until next <c>Reset</c>)
        /// </summary>
        string Bold { get; }

        /// <summary>
        /// Makes the text italic (until next <c>Reset</c>)
        /// </summary>
        string Italic { get; }

        /// <summary>
        /// Makes the text underlined (until next <c>Reset</c>)
        /// </summary>
        string Underline { get; }
        string Black { get; } string BlackBG {get;}
        string Red { get; } string RedBG {get;}
        string Green { get; } string GreenBG {get;}
        string Yellow { get; } string YellowBG {get;}
        string Blue { get; } string BlueBG {get;}
        string Magenta { get; } string MagentaBG {get;}
        string Cyan { get; } string CyanBG {get;}
        string White { get; } string WhiteBG {get;}

        /// <summary>
        /// Resets foreground and background color (doesn't affect the font style)
        /// </summary>
        string NoColor { get; }

        /// <summary>
        /// Displays the formatted text in the output.
        /// </summary>
        /// <param name="text">Text to be outputted with formatting.</param>
        void WriteFormatted(string text);
    }
}