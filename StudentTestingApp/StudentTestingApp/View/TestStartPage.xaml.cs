using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestStartPage : ContentPage, ITestStartView, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string TestName { get; private set; }
        public string QuestionCount { get; private set; }
        public string Duration { get; private set; }

        public TestStartPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = this;
        }

        private void StartTestClicked(object sender, EventArgs e)
        {
            OnStartTest?.Invoke();
        }

        #region ITestStartView
        public event Action OnStartTest;
        public string StudentName { get; set; }

        public void Show(IParentView parentView)
        {
            ((Page)parentView).Navigation.PushAsync(this);
        }

        public void ShowTestInfo(Test test)
        {
            TestName = test.Name;
            QuestionCount = test.QuestionCount.ToString();
            Duration = test.Duration == null ? "неограниченна" : $"{test.Duration} сек.";
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("TestName"));
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("QuestionCount"));
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("Duration"));
        }

        public void ShowError(string message)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Ошибка", message, "Назад");
            });
        }
        #endregion
    }
}