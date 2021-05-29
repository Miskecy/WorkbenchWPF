using Caliburn.Micro;
using System.Windows;
using WorkbenchWPF.ViewModels;

namespace WorkbenchWPF
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}
