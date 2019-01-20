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

        private void StartTestTapped(object sender, EventArgs e)
        {
            TestStarted?.Invoke();
        }

        #region ITestStartView

        public event Action TestStarted;
        public string StudentName => StudentNameEntry.Text;

        public void Show()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (Application.Current.MainPage == null)
                {
                    Application.Current.MainPage = new NavigationPage(this);
                }
                else
                {
                    Application.Current.MainPage.Navigation.PushAsync(this);
                }
            });
        }

        public void Close()
        {
            
        }

        public void ShowError(string message)
        {
            Device.BeginInvokeOnMainThread(() => { DisplayAlert("Ошибка", message, "Назад"); });
        }

        public void SetTest(string name, int questionCount, int? duration)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                NameLabel.Text = name;
                QuestionCountLabel.Text = $"Количество вопросов: {questionCount}";
                DurationLabel.Text = duration == null
                    ? "Продолжительность неограниченна"
                    : $"Продолжительность: {duration} сек.";
            });
        }

        #endregion
    }
}