namespace UpcomingHolidays;

using HolidaysDatabase;
using SharpViews;

public static partial class Screens
{
    public class CountrySelectScreen(IParentApp app) : Screen(app)
    {
        private bool DataFetched = false;

        private readonly ChoiceList<Country> CountryChoiceList = new([])
        {
            PaginationCount = 5
        };

        public override List<KeyboardAction>? KeyboardActions => DataFetched ? [
            new("up/down", "move selection"),
            new("a~z", "search by letter"),
            new("enter", "select country"),
            KeyboardAction.LineSeparator,
            new("esc", "exit app")
        ] : [];

        public override void DisplayScreen()
        {
            if (DataFetched)
            {
                Output.WriteLine(
                    C.UiFrame(
                        title: "Holidays",
                        inner:
                            C.CenteredText("Welcome. Select your country") +
                            C.List(
                                CountryChoiceList.PaginatedChoices.Select(c => $"{c.EnglishName} [{c.IsoCode}]"),
                                CountryChoiceList.SelectedIndex,
                                CountryChoiceList.PaginationStartIndex
                            ),

                        verticalScroll: true
                    )
                );
            }
        }

        public override async Task HandleInput()
        {
            if (!DataFetched)
            {
                if (!await ErrorUtils.TryOrHandleErrorAsync<HttpExceptionTranslator>(ShowLoadingAndFetchCountries))
                {
                    App.CloseScreen();
                }
            }
            else
            {
                var key = Input.GetKey(clearBuffer: true);
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        CountryChoiceList.MoveBackward();
                        break;
                    case ConsoleKey.DownArrow:
                        CountryChoiceList.MoveForward();
                        break;
                    case ConsoleKey.Enter:
                        if (CountryChoiceList.SelectedChoice is not null)
                        {
                            App.OpenScreen(
                                new HolidaysScreen(
                                    app: App,
                                    selectedCountry: CountryChoiceList.SelectedChoice
                                )
                            );
                        }
                        break;
                    case ConsoleKey.Escape:
                        App.CloseScreen(); break;
                    default:
                        char keyChar = char.ToUpper((char)key);
                        if (keyChar >= 'A' && keyChar <= 'Z' ) {
                            CountryChoiceList.MoveToChoice(c => c.EnglishName[0] == keyChar);
                        }
                        
                        break;
                }
            }
        }

        private async Task ShowLoadingAndFetchCountries()
        {
            F.WriteFormatted(F.Yellow + "Loading..." + F.Reset);
            CountryChoiceList.UpdateChoices([.. (await API.GetCountries()).OrderBy(c => c.EnglishName)]);
            DataFetched = true;
        }
    }
}