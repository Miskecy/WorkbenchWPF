using Caliburn.Micro;

namespace WorkbenchWPF.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        public MainViewModel()
        {
            ShowPageDashboard();
        }

        #region Pages
        public async void ShowPageDashboard()
        {
            await ActivateItemAsync(new DashboardViewModel());
        }

        public async void ShowPageTrade()
        {
            await ActivateItemAsync(new TradeViewModel());
        }

        public async void ShowPageLearning()
        {
            await ActivateItemAsync(new LearningViewModel());
        }
        #endregion
    }
}
