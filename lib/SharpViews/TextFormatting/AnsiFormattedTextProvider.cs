namespace SharpViews;

public static partial class TextFormatting
{
    /// <summary>
    /// An implementation of formatted text provider, which utilizes ANSI escape codes to change the colors and styling.
    /// </summary>
    /// <param name="outputHandler">Output handler interface that will be used to display the text.</param>
    public class AnsiFormattedTextProvider(IColorOutputHandler outputHandler) : IFormattedTextProvider
    {
        /// <inheritdoc/>
        public string Reset => EscCode("0");
        /// <inheritdoc/>
        public string Bold => EscCode("1");
        /// <inheritdoc/>
        public string Italic => EscCode("3");
        /// <inheritdoc/>
        public string Underline => EscCode("4");
        /// <inheritdoc/>
        public string Black => EscCode("30");
        /// <inheritdoc/>
        public string BlackBG => EscCode("40");
        /// <inheritdoc/>
        public string Red => EscCode("91");
        /// <inheritdoc/>
        public string RedBG => EscCode("101");
        /// <inheritdoc/>
        public string Green => EscCode("92");
        /// <inheritdoc/>
        public string GreenBG => EscCode("102");
        /// <inheritdoc/>
        public string Yellow => EscCode("93");
        /// <inheritdoc/>
        public string YellowBG => EscCode("103");
        /// <inheritdoc/>
        public string Blue => EscCode("94");
        /// <inheritdoc/>
        public string BlueBG => EscCode("104");
        /// <inheritdoc/>
        public string Magenta => EscCode("95");
        /// <inheritdoc/>
        public string MagentaBG => EscCode("105");
        /// <inheritdoc/>
        public string Cyan => EscCode("96");
        /// <inheritdoc/>
        public string CyanBG => EscCode("106");
        /// <inheritdoc/>
        public string White => EscCode("37");
        /// <inheritdoc/>
        public string WhiteBG => EscCode("47");

        /// <inheritdoc/>
        public string NoColor => EscCode("39;49");

        private static string EscCode(string num) => "\x1b[" + num + "m";

        /// <summary>
        /// An interface used to display the formatted text to the output.
        /// </summary>
        private IColorOutputHandler Output { get; } = outputHandler;

        /// <inheritdoc/>
        public void WriteFormatted(string text)
        {
            Output.Write(text + Reset);
        }
    }
}

