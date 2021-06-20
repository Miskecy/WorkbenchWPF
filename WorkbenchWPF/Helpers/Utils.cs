using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkbenchWPF.Helpers
{
    public class Utils
    {
        private string StringQuotes(string str)
        {
            // 1.500,00
            if (str.Contains(",")) return str.Replace(".", "").Replace(",", ".");
            else return str;
        }

        public decimal StringToDecimal(string str)
        {
            return Convert.ToDecimal(StringQuotes(str), CultureInfo.InvariantCulture);
        }

        public int StringToInteger(string str)
        {
            return Convert.ToInt32(str);
        }

        public decimal CalcWinrate(decimal win, decimal loss)
        {
            decimal x = win > 0 ? (win / (loss + win)) * 100 : 0;
            return x;
        }
    }
}
