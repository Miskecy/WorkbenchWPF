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

        public decimal StringToDecimal(string text)
        {            
            try
            {
                return Convert.ToDecimal(text, CultureInfo.InvariantCulture);
            }
            catch
            {
                decimal result;
                decimal.TryParse(text, out result);
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
