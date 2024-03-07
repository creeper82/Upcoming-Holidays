namespace HolidaysApp;

using HolidaysDatabase;
using CLI;

public static partial class App
{
    public static async Task CountrySelect()
    {
        
        bool retryConnection = true;

        while (retryConnection)
        {
            Console.Clear();
            Console.WriteLine("\nLoading...");
            List<Country> countries = [];

            string? error = null;

            try
            {
                countries = await API.GetCountries();
            }

            catch (HttpRequestException e)
            {
                error = "Error communicating with server. Check internet connection.\nMessage: " + e.Message;
            }

            catch (Exception) { error = "Unrecognized error occured. Sorry"; }

            if (error is null)
            {
                retryConnection = false;

                ChoiceList<Country> countryChoiceList = new(countries)
                {
                    PaginationCount = 5
                };

                countryChoiceList.choices = countryChoiceList.choices.OrderBy(c => c.EnglishName);

                bool running = true;

                while (running)
                {
                    countryChoiceList.CheckOutOfBoundsPointer();
                    Screens.CountrySelect(countryChoiceList.PaginatedChoices, countryChoiceList.selectedIndex, countryChoiceList.PaginationStartIndex);
                    running = await AppLogic.HandleCountrySelect(countryChoiceList);
                }
            }

            else
            {
                retryConnection = ErrorScreen("Holidays", error);
            }
        }


    }
}