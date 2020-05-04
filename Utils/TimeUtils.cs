using System;

namespace BerlinClock.Utils
{
    public static class TimeUtils
    {
        public static TimeSpan ConvertTimeStringTo24Format(string time)
        {
            if (time.Equals("24:00:00")) // an exception (There is no "24th hour" support in the DateTime class)
                return new TimeSpan(24, 0, 0);

            if (!DateTime.TryParse(time, out DateTime dateConverted))
                throw new ArgumentException("Parameter time can not be converted, because it is not valid");

            return TimeSpan.Parse(dateConverted.ToString("HH:mm:ss"));
        }
    }
}
