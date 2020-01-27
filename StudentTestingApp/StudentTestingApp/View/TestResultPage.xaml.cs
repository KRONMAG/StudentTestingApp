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

        private void HomeTapped(object sender, EventArgs e) =>
            DelayedResultUploadingSelected?.Invoke();

        #region ITestResultView

        public event Action DelayedResultUploadingSelected;

        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage = new NavigationPage(this));

        public void Close()
        {
            
        }

        public void SetTestResult(int elapsedTime, double result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ElapsedTimeLabel.Text = $"Затраченное время: {elapsedTime} сек.";
                ResultLabel.Text = $"{result}%";
                ResultProgressBar.Progress = 0;
            });
            var resultCopy = result;
            new Timer(state =>
            {
                if (resultCopy > 0)
                {
                    Device.BeginInvokeOnMainThread(() => ResultProgressBar.Progress += 0.01);
                    resultCopy -= 1;
                }
            }, null, 0, 20);
        }

        public void ShowMessage(string message) =>
            Device.BeginInvokeOnMainThread(() =>
                DisplayAlert(string.Empty, message, "Назад"));

        #endregion
    }
}