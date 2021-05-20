using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Framework
{
    public class ConvertDate
    {
        public static long ToDateInt64(DateTime dateTime)
        {
            int day = dateTime.Day;
            int month = dateTime.Month;
            int year = dateTime.Year;

            int dateInteger = (year * 10000) + (month*100) + day;

            return dateInteger;
        }

        public static long ToDateTime(DateTime dateTime)
        {
            int day = dateTime.Day;
            int month = dateTime.Month;
            int year = dateTime.Year;
            int hour = dateTime.Hour;
            int minute = dateTime.Minute;
            int second = dateTime.Second;

            long dateInteger = (year * 10000000000) + (month * 100000000) + (day * 1000000) + (hour * 10000) + (minute * 100) + second;

            return dateInteger;
        }

        public static DateTime ToDate(long dateInt)
        {
            long year = dateInt/10000;
            long month = (dateInt - (year * 10000))/100;
            long day = dateInt - ((year * 10000) + (month * 100));

            return new DateTime((int)year , (int)month, (int)day);
        }
    }
}
