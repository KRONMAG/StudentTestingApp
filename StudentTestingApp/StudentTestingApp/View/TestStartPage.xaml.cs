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
            BindingContext = this;
        }

        private void StartTestClicked(object sender, EventArgs e)
        {
            OnStartTest?.Invoke();
        }

        #region ITestStartView
        public event Action OnStartTest;
        public Test Test { get; set; }
        public string StudentName { get; set; }

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