using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestListPage : ContentPage, ITestListView
    {
        public class TestView
        {
            public string Name { get; set; }
        }

        public event Action OnSelectTest;
        public ObservableCollection<TestView> Tests { get; }
        public string SelectedTestName { get; private set; }

        public TestListPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Tests = new ObservableCollection<TestView>();
            BindingContext = this;
        }

        public void Show(IParentView parentView)
        {
            ((Page)parentView).Navigation.PushAsync(this);
        }

        public void ShowTest(string testName)
        {
            Tests.Add(new TestView() { Name = testName });
        }

        private void TestTapped(object sender, SelectedItemChangedEventArgs e)
        {
            SelectedTestName = ((TestView)((ListView)sender).SelectedItem).Name;
            OnSelectTest?.Invoke();
        }
    }
}