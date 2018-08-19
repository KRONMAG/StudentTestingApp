using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model;
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

        private void startTestClicked(object sender, EventArgs e)
        {
            OnStartTest?.Invoke();
        }

        #region ITestStartView
        public event Action OnStartTest;
        public string StudentName
        {
            get
            {
                return studentNameEntry.Text;
            }
        }

        public void Show()
        {
            App.Current.MainPage.Navigation.PushAsync(this);
        }

        public void ShowError(string message)
        {
            DisplayAlert("Ошибка", message, "Назад");
        }

        public void SetTest(Test test)
        {
            nameLabel.Text = test.Name;
            questionCountLabel.Text = $"Количество вопросов: {test.QuestionCount}";
            if (test.Duration == null) durationLabel.Text = "Продолжительность неограниченна";
            else durationLabel.Text = $"Продолжительность: {test.Duration} сек.";
        }
        #endregion
    }
}