using System;
using System.Collections.Generic;

namespace Business_Logic.Helpers
{
    public static class DateHelper
    {
        public static int ToInt(this DateTime dateTime)
        {
            return dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day;
        }

        public static List<int> ToInts(this List<DateTime> dateTimes)
        {
            var intDates = new List<int>();

            dateTimes.ForEach(date => intDates.Add(ToInt(date)));

            return intDates;
        }

        public static List<DateTime> ConvertIntsToDatetimes(this List<int> intDates)
        {
            var dates = new List<DateTime>();

            foreach (var integer in intDates)
            {
                var day = integer % 100;
                var month = (integer / 100) % 100;
                var year = integer / 10000;

                dates.Add(new DateTime(year, month, day));
            }

            return dates;
        }

        public static DateTime ConvertIntToDatetime(this int intDate)
        {

            if (intDate == 0) return new DateTime();

            var day = intDate % 100;
            var month = (intDate / 100) % 100;
            var year = intDate / 10000;

            return new DateTime(year, month, day); ;
        }
    }
}
