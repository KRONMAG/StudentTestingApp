using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestStartPage : ContentPage, ITestStartView
    {
        public TestStartPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void StartTestClicked(object sender, EventArgs e) =>
            StartTestSelected?.Invoke();

        #region ITestStartView

        public event Action StartTestSelected;

        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage.Navigation.PushAsync(this));

        public void ShowMessage(string message) =>
            Device.BeginInvokeOnMainThread(() =>
                DisplayAlert(string.Empty, message, "Назад"));

        public void SetTest(string name, int questionCount, int? duration)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                NameLabel.Text = name;
                QuestionCountLabel.Text = $"Количество вопросов: {questionCount}";
                DurationLabel.Text = duration == null
                    ? "Продолжительность неограничена"
                    : $"Продолжительность: {duration} сек.";
            });
        }

        #endregion
    }
}