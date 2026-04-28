using System;
using System.Globalization;
using TasksManager.Shared.GlobalConstants;

namespace TasksManager.Shared.Helpers
{
    public static class DateHelper
    {
        public static DateTime? TryParseDate(string? value)
        {
            if (string.IsNullOrEmpty(value)) return null;
            if (DateTime.TryParseExact(value, DateFormats.FullDateTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out var full))
                return full;
            if (DateTime.TryParseExact(value, DateFormats.ShortDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;
            return null;
        }
    }
}
