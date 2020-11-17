using System;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestResultsItemPage : PopupPage
    {
        public event Action ShareTestResult;
        public event Action RemoveTestResult;

        public TestResultsItemPage(Tuple<int, string, string, DateTime, int, decimal> result)
        {
            BindingContext = result;
            InitializeComponent();
        }

        private async void ShareTestResultClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
            ShareTestResult?.Invoke();
        }

        private async void RemoveTestResultClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
            RemoveTestResult?.Invoke();
        }

        private async void BackClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}