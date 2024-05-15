namespace UpcomingHolidays.CLI;
using HolidaysDatabase;

public static class Components {
    public static string HolidayList(IEnumerable<Holiday> holidays, bool useEnglish)
    {
        string result = "";

        foreach (Holiday h in holidays)
        {
            result += (useEnglish ? h.EnglishName : h.NativeName) + "\n";

            result += "<blue>";

            if (h.StartDate == h.EndDate)
            {
                result += h.StartDate.ToLongDateString();
            }
            else
            {
                result += h.StartDate.ToShortDateString() + " ~ " + h.EndDate.ToShortDateString();
            }

            result += "</>";

            result += "\n\n";
        }

        return result;
    }
}