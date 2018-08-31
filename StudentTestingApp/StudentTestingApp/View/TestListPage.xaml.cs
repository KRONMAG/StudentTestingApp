using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestListPage : ContentPage, ITestListView
    {
        public TestListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void TestTapped(object sender, ItemTappedEventArgs e)
        {
            OnSelectTest?.Invoke();
        }

        #region ITestListView

        public event Action OnSelectTest;
        public int SelectedTestId => ((Tuple<int, string>) TestsListView.SelectedItem).Item1;

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

        public void SetTests(IEnumerable<Tuple<int, string>> tests)
        {
            Device.BeginInvokeOnMainThread(() => { TestsListView.ItemsSource = tests; });
        }

        #endregion
    }
}