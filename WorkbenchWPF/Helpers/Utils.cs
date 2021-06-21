using System;
using System.Globalization;

namespace WorkbenchWPF.Helpers
{
    public class Utils
    {
        private static string StringQuotes(string str)
        {
            try
            {
                if(!string.IsNullOrEmpty(str))
                {
                    // 1.500,00
                    if (str.Contains(",")) return str.Replace(".", "").Replace(",", ".");
                    else return str;
                }
                else
                {
                    return "0.00";
                }
            }
            catch
            {
                return "0.00";
            }
        }

        public static decimal StringToDecimal(string str)
        {
            try
            {
                return Convert.ToDecimal(StringQuotes(str), CultureInfo.InvariantCulture);
            }
            catch
            {
                return 0.00m;
            }
        }

        public static int StringToInteger(string str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch
            {
                return 0;
            }
        }

        public static decimal CalcWinrate(decimal win, decimal loss)
        {
            try
            {
                decimal x = win > 0 ? (win / (loss + win)) * 100 : 0;
                return x;
            }
            catch
            {
                return 0;
            }
        }
    }
}
