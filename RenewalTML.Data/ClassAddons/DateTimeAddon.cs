using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenewalTML.Shared.Exstention.ClassAddons
{
    public class DateTimeAddon
    {
        public static string NowDateTimeStrings() => DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
        public static DateTime StringToDateTime(string str) => DateTime.ParseExact(str, "yyyy.MM.dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        public static string StringToDateTimeFormat(string str)
        {
            if(String.IsNullOrEmpty(str))
                return "не определено";

            try
            {
                var dateTime = DateTime.ParseExact(str, "yyyy.MM.dd HH:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture);

                const int SECOND = 1;
                const int MINUTE = 60 * SECOND;
                const int HOUR = 60 * MINUTE;
                const int DAY = 24 * HOUR;
                const int MONTH = 30 * DAY;

                var ts = new TimeSpan(DateTime.Now.Ticks - dateTime.Ticks);
                double delta = Math.Abs(ts.TotalSeconds);

                if (delta >= 0 && delta <= 5)
                {
                    return "только что";
                }

                if (delta < 1 * MINUTE)
                {
                    if ((ts.Seconds % 10) == 1) return ts.Seconds + " секунду назад";
                    else if ((ts.Seconds % 10) >= 2 && (ts.Seconds % 10) <= 4) return ts.Seconds + " секунды назад";
                    return ts.Seconds + " секунд назад";
                }

                if (delta < 2 * MINUTE)
                    return "минуту назад";

                if (delta < 59 * MINUTE)
                {
                    if ((ts.Minutes % 10) == 1) return ts.Minutes + " минуту назад";
                    else if ((ts.Minutes % 10) >= 2 && (ts.Minutes % 10) <= 4) return ts.Minutes + " минуты назад";
                    return ts.Minutes + " минут назад";
                }

                if (delta < 95 * MINUTE)
                    return "час назад";

                if (delta < 24 * HOUR)
                {
                    if (Math.Abs(ts.Hours) == 21) return Math.Abs(ts.Hours) + " час назад";
                    else if ((Math.Abs(ts.Hours) % 10) >= 2 && (Math.Abs(ts.Hours) % 10) <= 4) return Math.Abs(ts.Hours) + " часа назад";
                    return Math.Abs(ts.Hours) + " часов назад";
                }

                if (delta < 48 * HOUR)
                    return "Вчера";

                if (delta < 72 * HOUR)
                    return "позавчера";

                if (delta < 30 * DAY)
                {
                    if (ts.Days == 11 || ts.Days == 12 || ts.Days == 13 || ts.Days == 14) return ts.Days + " дней назад";
                    else if (ts.Days == 21) return "21 день назад";
                    else if ((ts.Days % 10) >= 2 && (ts.Days % 10) <= 4) return ts.Days + " дня назад";
                    return ts.Days + " дней назад";
                }

                if (delta < 12 * MONTH)
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    if (months == 1) return "месяц назад";
                    if (months >= 2 && months <= 4) return months + " месяца назад";
                    return months + " месяцев назад";
                }
                else
                {
                    int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                    if (years == 11 || years == 12 || years == 13 || years == 14) return years + " лет назад";
                    else if ((years % 10) == 1) return years + " год назад";
                    else if ((years % 10) >= 2 && (years % 10) <= 4) return years + " года назад";
                    return years + " лет назад";
                }
            }
            catch (Exception)
            {
                return "не определено";
            }
        }
    }
}
