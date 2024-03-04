namespace CLI;

// KeyboardAction represents a key you have to press and a name of action it'll trigger
public class KeyboardAction(string key, string optionText)
{
    public string Key = key;
    public string OptionText = optionText;

    public override string ToString()
    {
        // Empty option string is used as a line separator
        return (Key == "" && OptionText == "") ? "" : $"[ {Key} ] - {OptionText}";
    }

    public static KeyboardAction LineSeparator => new("", "");
}

// List of commonly used keyboard actions among app screens
public static class KeyboardActions
{
    public static List<KeyboardAction> LanguageSelectScreen { get; } = [
        new("up/down", "move selection"),
        new("enter", "select country"),
        KeyboardAction.LineSeparator,
        new("esc", "exit app"),
    ];
}