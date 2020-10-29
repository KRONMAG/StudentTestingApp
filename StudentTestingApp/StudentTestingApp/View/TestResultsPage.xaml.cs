using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestResultsPage : ContentPage, ITestResultsView
    {
        public TestResultsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void TestResultTapped(object sender, ItemTappedEventArgs e)
        {
            var result = GetSelectedTestResult();
            var needToRemoveResult = await DisplayAlert
            (
                "Результат",
                $"Предмет: {result.Item2}\n" +
                $"Тест: {result.Item3}\n" +
                $"Дата: {result.Item4}\n" +
                $"Время прохождения: {result.Item5} сек.\n" +
                $"Результат: {result.Item6}%",
                "Удалить",
                "Назад"
            );
            if (needToRemoveResult)
                RemoveTestResult?.Invoke();
        }

        private async void RemoveAllTestResultsClicked(object sender, EventArgs e)
        {
            var needToRemoveAllResults = await DisplayAlert
            (
                "Удаление",
                "Вы действительно хотите удалить все результаты тестирования?",
                "Да",
                "Нет"
            );
            if (needToRemoveAllResults)
                RemoveAllTestResults?.Invoke();
        }

        private Tuple<int, string, string, DateTime, int, double> GetSelectedTestResult() =>
            ((Tuple<int, string, string, DateTime, int, double>)TestResultsListView.SelectedItem);

        #region ITestResultsView

        public int TestResultToRemoveId =>
            GetSelectedTestResult().Item1;

        public event Action RemoveTestResult;
        public event Action RemoveAllTestResults;

        public void SetTestResults(IReadOnlyList<Tuple<int, string, string, DateTime, int, double>> results)
        {
            var isEmpty = !results.Any();
            Device.BeginInvokeOnMainThread(() =>
            {
                EmptyTestResultsLabel.IsVisible = isEmpty;
                TestResultsListView.IsVisible = !isEmpty;
                TestResultsListView.ItemsSource = results;
            });
        }

        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage.Navigation.PushAsync(this));

        #endregion
    }
}