using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model;
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
            Tests = new ObservableCollection<Test>();
            BindingContext = this;
        }

        private void TestTapped(object sender, ItemTappedEventArgs e)
        {
            OnSelectTest?.Invoke();
        }

        #region ITestListView
        public event Action OnSelectTest;
        public ICollection<Test> Tests { get; private set; }
        public Test SelectedTest { get; set; }

        public void Show(IParentView parentView)
        {
            ((Page)parentView).Navigation.PushAsync(this);
        }
        #endregion
    }
}