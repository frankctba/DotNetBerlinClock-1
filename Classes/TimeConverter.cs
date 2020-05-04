using System;
using System.Collections.Generic;
using System.Linq;
using BerlinClock.Classes;
using BerlinClock.Utils;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            var result = new List<string>();

            var timeForBerlinClock = TimeUtils.ConvertTimeStringTo24Format(aTime);

            result.Add(GetLampForSeconds(timeForBerlinClock.Seconds));
            result.Add(GetLampsForHours((int)timeForBerlinClock.TotalHours, 1));
            result.Add(GetLampsForHours((int)timeForBerlinClock.TotalHours, 2));
            result.Add(GetLampsForMinutesThirdRow(timeForBerlinClock.Minutes));
            result.Add(GetLampsForMinutesFourthRow(timeForBerlinClock.Minutes));

            return StringUtils.JoinStringLines(result.ToArray());
        }

        private string GetLampForSeconds(int seconds)
        {
            if ((seconds < 0) || (seconds > 59))
                throw new ArgumentException("Parameter seconds must be between 0 and 59");

            return seconds % 2 == 0 ? Constants.SwitchedOnYellowCharacter.ToString()
            : Constants.SwitchedOffCharacter.ToString();
        }

        private string GetLampsForHours(int hours, int nRow)
        {
            if ((hours < 0) || (hours > 24))
                throw new ArgumentException("Parameter hours must be between 0 and 24");

            if ((nRow != 1) && (nRow != 2))
                throw new ArgumentException("Parameter nRow must be 1 or 2");

            // every switched on lamp count 5 hours
            var lampsArray = Enumerable.Repeat(Constants.SwitchedOffCharacter, 4).ToArray();
            var numberOfSwitchedOnLamps = nRow == 1 ? hours / 5 : hours % 5;
            for (var nLamp = 0; nLamp < numberOfSwitchedOnLamps; nLamp++)
                lampsArray[nLamp] = Constants.SwitchedOnRedCharacter;

            return string.Join("", lampsArray);
        }

        private string GetLampsForMinutesThirdRow(int minutes)
        {
            if ((minutes < 0) || (minutes > 59))
                throw new ArgumentException("Parameter minutes must be between 0 and 59");

            // every switched on lamp count 5 minutes
            var lampsArray = Enumerable.Repeat(Constants.SwitchedOffCharacter, 11).ToArray();
            var numberOfSwitchedOnLamps = minutes / 5;
            for (var nLamp = 0; nLamp < numberOfSwitchedOnLamps; nLamp++)
                lampsArray[nLamp] = ((nLamp == 2) || (nLamp == 5) || (nLamp == 8)) ?
                    Constants.SwitchedOnRedCharacter : Constants.SwitchedOnYellowCharacter;

            return string.Join("", lampsArray);
        }

        private string GetLampsForMinutesFourthRow(int minutes)
        {
            if ((minutes < 0) || (minutes > 59))
                throw new ArgumentException("Parameter minutes must be between 0 and 59");

            // every switched on lamp count 1 minute
            var lampsArray = Enumerable.Repeat(Constants.SwitchedOffCharacter, 4).ToArray();
            var numberOfSwitchedOnLamps = minutes % 5;
            for (var nLamp = 0; nLamp < numberOfSwitchedOnLamps; nLamp++)
                lampsArray[nLamp] = Constants.SwitchedOnYellowCharacter;

            return string.Join("", lampsArray);
        }
    }
}
