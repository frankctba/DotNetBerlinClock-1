using System;

namespace BerlinClock.Utils
{
    public static class StringUtils
    {
        public static string JoinStringLines(string[] lines)
        {
            if (lines.Length <= 0)
                return String.Empty;

            return String.Join(Environment.NewLine, lines);
        }
    }
}
