using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestResultPage : ContentPage, ITestResultView
    {
        public TestResultPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void GoToMainPageClicked(object sender, EventArgs e) =>
            GoToMainViewSelected?.Invoke();

        #region ITestResultView

        public event Action GoToMainViewSelected;

        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage = new NavigationPage(this));

        public void SetTestResult(int elapsedTime, double score)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ElapsedTimeLabel.Text = $"Затраченное время: {elapsedTime} сек.";
                ScoreLabel.Text = $"{score}%";
                ScoreProgressBar.Progress = 0;
            });
            new Timer(state =>
            {
                if (ScoreProgressBar.Progress * 100 < score)
                    Device.BeginInvokeOnMainThread(() => ScoreProgressBar.Progress += 0.01);
            }, null, 0, 20);
        }

        public void ShowMessage(string message) =>
            Device.BeginInvokeOnMainThread(() =>
                DisplayAlert(string.Empty, message, "Назад"));

        #endregion
    }
}