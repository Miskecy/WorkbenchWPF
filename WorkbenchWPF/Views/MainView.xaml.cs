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
using System.Windows.Shapes;

namespace WorkbenchWPF.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        struct Colors
        {
            public static SolidColorBrush Purple = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6458FF"));
            public static SolidColorBrush gray = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c1c1c1"));
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void SetMinimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void SetMaximize(object sender, RoutedEventArgs e)
        {

            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }
        private void SetClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnHouverBtnSettings(object sender, MouseEventArgs e)
        {
            btnSettings.Background = Colors.gray;
            imageButtonSttings.Source = new BitmapImage(new Uri(@"/Assets/Icons/settings_50px_purple.png", UriKind.Relative));
            textButtonSttings.Foreground = Colors.Purple;
        }

        private void NormalBtnSettings(object sender, MouseEventArgs e)
        {
            btnSettings.Background = Colors.Purple;
            imageButtonSttings.Source = new BitmapImage(new Uri(@"/Assets/Icons/settings_50px_gray.png", UriKind.Relative));
            textButtonSttings.Foreground = Colors.gray;
        }
    }
}
