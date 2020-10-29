using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, IMainView
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void SubjectsViewClicked(object sender, EventArgs e) =>
            SubjectsViewSelected?.Invoke();

        private void TestResultsViewClicked(object sender, EventArgs e) =>
            TestResultsViewSelected?.Invoke();

        private void UpdateTestsClicked(object sender, EventArgs e) =>
            UpdateTestsSelected?.Invoke();

        #region IMainView

        public event Action SubjectsViewSelected;
        public event Action TestResultsViewSelected;
        public event Action UpdateTestsSelected;

        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage = new NavigationPage(this)
                {
                    BarBackgroundColor = Color.FromHex("212121")
                });

        public void ShowMessage(string message) =>
            Device.BeginInvokeOnMainThread(() =>
                DisplayAlert(string.Empty, message, "Назад"));

        #endregion
    }
}