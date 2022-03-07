using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenewalTML.Shared.Exstention.ClassAddons
{
    public class CurrencyAddon
    {
        public static string Format(int number, bool catchdot = false)
        {
            return FormatNumberString(Convert.ToString(number), catchdot);
        }

        private static string FormatNumberString(string number, bool catchdot)
        {
            if (number.Length < 4)
            {
                return number;
            }

            if (number.Length < 7)
            {
                return FormatThousands(number, catchdot);
            }

            if (!catchdot && number.Length >= 9) return "99.99M+";
            else if (catchdot && number.Length >= 9) return "99M+";

            return FormatGeneral(number, catchdot);
        }

        private static string FormatThousands(string number, bool catchdot)
        {
            string leadingNumbers = number.Substring(0, number.Length - 3);
            string decimals = number.Substring(number.Length - 3);

            if(catchdot) return CreateNumericalFormat(leadingNumbers, "", "K");
            else return CreateNumericalFormat(leadingNumbers, FormatDecimal(decimals), "K");
        }

        private static string FormatDecimal(string dec)
        {
            var ret = Convert.ToString((Convert.ToInt32(dec) / 10));

            if (ret == "0" || ret == "00")
                return "";
            else return ret;
        }

        private static string CreateNumericalFormat(string leadingNumbers, string decimals, string suffix)
        {
            if (decimals == "") return String.Format("{0}{1}", leadingNumbers, suffix);
            else return String.Format("{0}.{1}{2}", leadingNumbers, decimals, suffix);
        }

        private static string FormatGeneral(string number, bool catchdot)
        {
            int amountOfLeadingNumbers = (number.Length - 7) % 3 + 1;
            string leadingNumbers = number.Substring(0, amountOfLeadingNumbers);
            string decimals = number.Substring(amountOfLeadingNumbers, 3);

            if(catchdot) return CreateNumericalFormat(leadingNumbers, "", GetSuffixForNumber(number));
            else return CreateNumericalFormat(leadingNumbers, FormatDecimal(decimals), GetSuffixForNumber(number));
        }

        private static string GetSuffixForNumber(string number)
        {
            int numberOfThousands = (number.Length - 1) / 3;

            switch (numberOfThousands)
            {
                case 1:
                    return "K";
                case 2:
                    return "M";
                case 3:
                    return "B";
                case 4:
                    return "T";
                case 5:
                    return "Q";
                default:
                    return GetProceduralSuffix(numberOfThousands - 5);
            }
        }

        private static string GetProceduralSuffix(int value)
        {
            StringBuilder sb = new StringBuilder();

            while (value > 0)
            {
                int digit = value % 26;

                sb.Append((char)('a' + digit));
                value /= 26;
            }

            if (sb.Length == 0)
            {
                sb.Append('a');
            }

            Reverse(sb);
            return sb.ToString();
        }

        private static void Reverse(StringBuilder sb)
        {
            for (int i = 0, j = sb.Length - 1; i < sb.Length / 2; i++, j--)
            {
                char chT = sb[i];

                sb[i] = sb[j];
                sb[j] = chT;
            }
        }
    }
}
