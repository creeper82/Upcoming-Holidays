using SharpViews;
using UpcomingHolidays;

Console.OutputEncoding = System.Text.Encoding.UTF8;

IFullOutputHandler Output = new ConsoleOutputHandler();
IInputHandler Input = new ConsoleInputHandler();
TextFormatting.IFormattedTextProvider FT = new TextFormatting.AnsiFormattedTextProvider(Output);

SharpViewsApp app = new(Input, Output, FT);

app.OpenScreen(new Screens.CountrySelectScreen(app));

await app.StartAsync();