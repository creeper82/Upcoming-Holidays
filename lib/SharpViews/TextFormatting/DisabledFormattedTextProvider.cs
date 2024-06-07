#pragma warning disable CS1591

namespace SharpViews;

public static partial class TextFormatting {
    /// <summary>
    /// A dummy formatted text provider, which ignores all formatting rules. The text will be always displayed with default colors,
    /// and no font styling. Might be usable for debug purposes, or for places where it is impossible to output formatted text.
    /// </summary>
    public class DisabledFormattedTextProvider : IFormattedTextProvider
    {
        public string Reset => "";
        public string Bold => "";
        public string Italic => "";
        public string Underline => "";
        public string Black => "";
        public string BlackBG => "";
        public string Red => "";
        public string RedBG => "";
        public string Green => "";
        public string GreenBG => "";
        public string Yellow => "";
        public string YellowBG => "";
        public string Blue => "";
        public string BlueBG => "";
        public string Magenta => "";
        public string MagentaBG => "";
        public string Cyan => "";
        public string CyanBG => "";
        public string White => "";
        public string WhiteBG => "";
        public string NoColor => "";

        public void WriteFormatted(string _) {}
    }
}