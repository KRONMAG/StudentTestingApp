using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestListPage : ContentPage, ITestListView
    {
        public ObservableCollection<Test> tests;

        public TestListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            tests = new ObservableCollection<Test>();
            testsListView.ItemsSource = tests;
        }

        private void testTapped(object sender, ItemTappedEventArgs e)
        {
            OnSelectTest?.Invoke();
        }

        #region ITestListView
        public event Action OnSelectTest;
        public Test SelectedTest
        {
            get
            {
                return (Test)testsListView.SelectedItem;
            }
        }

        public void Show(IParentView parentView)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ((Page)parentView).Navigation.PushAsync(this);
            });
        }

        public void Close()
        {

        }

        public void SetTests(IEnumerable<Test> tests)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.tests.Clear();
                tests.ToList().ForEach((test) =>
                {
                    this.tests.Add(test);
                });
            });
        }
        #endregion
    }
}