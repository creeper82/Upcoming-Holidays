namespace UpcomingHolidays;

using System.Threading.Tasks;
using HolidaysDatabase;
using SharpViews;

public static partial class Screens
{
    public class HolidaysScreen(IParentApp app, Country selectedCountry) : Screen(app)
    {
        private bool UseEnglish = false;
        private bool DataFetched = false;

        private readonly ScrollableList<Holiday> HolidaysScrollList = new([])
        {
            PaginationCount = 5
        };


        public override List<KeyboardAction>? KeyboardActions => [
            new("s", "switch language (english <> native)"),
                new("up/down", "scroll list"),
                KeyboardAction.LineSeparator,
                new("esc", "go back")
        ];

        public override void DisplayScreen()
        {
            if (DataFetched)
            {
                F.WriteFormatted(
                    C.UiFrame(
                        inner: HolidayList(HolidaysScrollList.PaginatedChoices, UseEnglish),

                        title: $"Holidays - {selectedCountry.EnglishName}",
                        verticalScroll: true
                    )
                );
                if (UiHeight < 30)
                {
                    F.WriteFormatted(F.Yellow + "Please increase the window height." + F.Reset);
                }
            }
        }

        private string HolidayList(IEnumerable<Holiday> holidays, bool useEnglish)
        {
            string result = "";

            foreach (Holiday h in holidays)
            {
                result += (useEnglish ? h.EnglishName : h.NativeName) + "\n";

                result += F.Blue;

                if (h.StartDate == h.EndDate)
                {
                    result += h.StartDate.ToLongDateString();
                }
                else
                {
                    result += h.StartDate.ToShortDateString() + " ~ " + h.EndDate.ToShortDateString();
                }

                result += F.Reset + "\n\n";
            }

            return result;
        }

        public override async Task HandleInput()
        {
            if (!DataFetched)
            {
                await ErrorUtils.TryOrHandleErrorAsync<HttpExceptionTranslator>(ShowLoadingAndFetchHolidays);
            }
            else
            {
                switch (Input.GetKey(clearBuffer: true))
                {
                    case ConsoleKey.UpArrow:
                        HolidaysScrollList.MoveBackward(); break;
                    case ConsoleKey.DownArrow:
                        HolidaysScrollList.MoveForward(); break;
                    case ConsoleKey.S:
                        UseEnglish = !UseEnglish; break;
                    case ConsoleKey.Escape:
                        App.CloseScreen(); break;
                }
            }
        }

        private async Task ShowLoadingAndFetchHolidays()
        {
            F.WriteFormatted(F.Yellow + "Loading..." + F.Reset);

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            DateOnly dateFrom = today;
            DateOnly dateTo = new(today.Year + 1, 12, 31);

            HolidaysScrollList.UpdateChoices(await API.GetHolidays(selectedCountry.IsoCode, dateFrom, dateTo));

            DataFetched = true;
        }
    }
}