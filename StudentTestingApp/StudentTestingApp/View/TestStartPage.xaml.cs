using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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

        public void Show(IParentView parentView)
        {
            ((Page)parentView).Navigation.PushAsync(this);
        }

        public void ShowTestInfo(string testName, int questionCount, int? duration)
        {
            TestName = testName;
            QuestionCount = questionCount.ToString();
            Duration = duration == null ? "неограниченна" : $"{duration} сек.";
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("TestName"));
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("QuestionCount"));
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs("Duration"));
        }
    }
}