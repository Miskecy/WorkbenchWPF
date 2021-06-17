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
            return str.Replace(".", "").Replace(",", ".");
        }

        public decimal StringToDecimal(string str)
        {            
            try
            {
                return Convert.ToDecimal(StringQuotes(str), CultureInfo.InvariantCulture);
            }
            catch
            {
                decimal result;
                decimal.TryParse(StringQuotes(str), out result);
                return result;
            }
        }

        public int StringToInteger(string text)
        {
            try
            {
                return Convert.ToInt32(text);
            }
            catch
            {
                int result;
                int.TryParse(text, out result);
                return result;
            }
        }

        public decimal CalcWinrate(decimal win, decimal loss)
        {
            decimal x = win > 0 ? (win / (loss + win)) * 100 : 0;
            return x;
        }
    }
}
