using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage, ISettingsView
    {
        public SettingsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void UpdateTestsClicked(object sender, EventArgs e) =>
            UpdateTests?.Invoke();

        private void LogInToDnevnikClicked(object sender, EventArgs e) =>
            TryLogInToDnevnik?.Invoke();

        private void LogOutFromDnevnikClicked(object sender, EventArgs e) =>
            LogOutFromDnevnik?.Invoke();

        #region ISettingsView

        public event Action UpdateTests;

        public event Action TryLogInToDnevnik;

        public event Action LogOutFromDnevnik;

        public string Login
        {
            get => LoginEntry.Text;
            set => Device.BeginInvokeOnMainThread(() => LoginEntry.Text = value);
        }

        public string Password
        {
            get => PasswordEntry.Text;
            set => Device.BeginInvokeOnMainThread(() => PasswordEntry.Text = value);
        }

        public void ShowExpirationDate(DateTime? date = null) =>
            Device.BeginInvokeOnMainThread(() =>
            {
                if (date != null)
                    ExpirationDateLabel.Text = date.ToString();
                else
                    ExpirationDateLabel.Text = "вход не выполнен";
            });

        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage.Navigation.PushAsync(this));

        #endregion
    }
}