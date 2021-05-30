using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkbenchWPF.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
  
        public DashboardView()
        {
            InitializeComponent();    

        }

        

        //private Color HeatMapColor(double value, double min, double max)
        //{
        //    Color firstColour = Color.FromRgb(100, 88, 255);
        //    Color secondColour = Color.FromRgb(122, 54, 156);

        //    // Example: Take the RGB
        //    //135-206-250 // Light Sky Blue
        //    // 65-105-225 // Royal Blue
        //    // 70-101-25 // Delta

        //    int rOffset = Math.Max(firstColour.R, secondColour.R);
        //    int gOffset = Math.Max(firstColour.G, secondColour.G);
        //    int bOffset = Math.Max(firstColour.B, secondColour.B);

        //    int deltaR = Math.Abs(firstColour.R - secondColour.R);
        //    int deltaG = Math.Abs(firstColour.G - secondColour.G);
        //    int deltaB = Math.Abs(firstColour.B - secondColour.B);

        //    double val = (value - min) / (max - min);
        //    int r = rOffset - Convert.ToByte(deltaR * (1 - val));
        //    int g = gOffset - Convert.ToByte(deltaG * (1 - val));
        //    int b = bOffset - Convert.ToByte(deltaB * (1 - val));

        //    return Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
        //}

        public Color HeatMapColor(decimal value, decimal min, decimal max)
        {
            decimal val = (value - min) / (max - min);
            int r = Convert.ToByte(255 * val);
            int g = Convert.ToByte(255 * (1 - val));
            int b = 0;

            return Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
        }

        static Color CreateHeatColor(int value, decimal max)
        {
            if (max == 0) max = 1M;
            decimal pct = value / max;
            Color color = new Color();

            color.A = 255;

            if (pct < 0.34M)
            {
                color.R = (byte)(128 + (127 * Math.Min(3 * pct, 1M)));
                color.G = 0;
                color.B = 0;
            }
            else if (pct < 0.67M)
            {
                color.R = 255;
                color.G = (byte)(255 * Math.Min(3 * (pct - 0.333333M), 1M));
                color.B = 0;
            }
            else
            {
                color.R = (byte)(255 * Math.Min(3 * (1M - pct), 1M));
                color.G = 255;
                color.B = 0;
            }

            return color;
        }
    }
}
