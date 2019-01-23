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

        private void SubjectListViewClicked(object sender, EventArgs e)
        {
            SubjectListViewSelected?.Invoke();
        }

        #region IMainView

        public event Action SubjectListViewSelected;

        public void Show()
        {
            Device.BeginInvokeOnMainThread(() => Application.Current.MainPage = new NavigationPage(this)
            { BarBackgroundColor = Color.FromHex("212121") });
        }

        public void Close()
        {

        }

        #endregion
    }
}