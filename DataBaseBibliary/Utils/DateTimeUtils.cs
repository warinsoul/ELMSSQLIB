using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBibliary.Utils
{
    public  static class DateTimeUtils
    {
        public enum Accuracy { Year, Quarter, Month, Week, Day, Hour, Minute, Second };


        public static DateTime TruncateDate(this DateTime inDate, Accuracy accuracy)
        {
            switch (accuracy)
            {
                case Accuracy.Year:
                    return new DateTime(inDate.Year, 1, 1);
                case Accuracy.Quarter:
                    int i = inDate.Month % 3;
                    return new DateTime(inDate.Year, inDate.Month - i + 1, 1);
                case Accuracy.Month:
                    return new DateTime(inDate.Year, inDate.Month, 1);
                case Accuracy.Week:
                    return new DateTime(inDate.Year, inDate.Month, inDate.Day).AddDays(-(int)inDate.DayOfWeek);
                case Accuracy.Day:
                    return new DateTime(inDate.Year, inDate.Month, inDate.Day);
                case Accuracy.Hour:
                    return new DateTime(inDate.Year, inDate.Month, inDate.Day, inDate.Hour, 0, 0);
                case Accuracy.Minute:
                    return new DateTime(inDate.Year, inDate.Month, inDate.Day, inDate.Hour, inDate.Minute, 0);
                case Accuracy.Second:
                    return new DateTime(inDate.Year, inDate.Month, inDate.Day, inDate.Hour, inDate.Minute, inDate.Second);
                default:
                    throw new ArgumentOutOfRangeException("accuracy");
            }
        }
    }
}
