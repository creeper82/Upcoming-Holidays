namespace CLI;

using static Components;

public partial class Screens
{
    public static void CountrySelect()
    {
        ClearConsole();
        // Display menu
        Console.WriteLine(
            UiFrame(
                    inner: CenteredText("Welcome. Select your country") // +
                    
                        
                    ,
                    title: "Flashcards",
                    verticalScroll: true
                    )
        );

        // Display keyboard actions
        Console.WriteLine(
            KeyboardActionList(KeyboardActions.CountrySelectScreen)
        );
    }
}