namespace UpcomingHolidays;

using HolidaysDatabase;
using CLI;
using SharpViews;

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
                if (countries.Count == 0) error = "Server responded with empty data.";
            }

            catch (Exception e) when (e.InnerException is TimeoutException)
            {
                error = "The request has timed out. Check internet connection, or try again if it is slow";
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

                countryChoiceList.UpdateChoices(countryChoiceList.Choices.OrderBy(c => c.EnglishName));

                bool running = true;

                while (running)
                {
                    Screens.CountrySelect(countryChoiceList.PaginatedChoices, countryChoiceList.SelectedIndex, countryChoiceList.PaginationStartIndex);
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