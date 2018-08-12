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

        #region INotifyPropertyChanged
        new public event PropertyChangedEventHandler PropertyChanged;

        new private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region ITestStartView
        public event Action OnStartTest;
        public Test Test
        {
            get
            {
                return test;
            }
            set
            {
                test = value;
                OnPropertyChanged("Test");
            }
        }
        public string StudentName { get; set; }

        private Test test;

        public void Show(IParentView parentView)
        {
            ((Page)parentView).Navigation.PushAsync(this);
        }

        public void ShowError(string message)
        {
            DisplayAlert("Ошибка", message, "Назад");
        }
        #endregion
    }
}