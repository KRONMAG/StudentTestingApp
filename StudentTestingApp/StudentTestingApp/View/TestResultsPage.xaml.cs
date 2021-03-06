﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница со списком результатов тестирования
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestResultsPage : ContentPage, ITestResultsView
    {
        /// <summary>
        /// Инициализация страницы
        /// </summary>
        public TestResultsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Обработчик нажатия на элемент списка результатов тестирования
        /// Показывает подробные данные о выбранном результате
        /// с возможностью выбора опции его удаления
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
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

        /// <summary>
        /// Обработчик нажатия кнопки удаления всех результатов тестирования
        /// Запрашивает у пользователя разрешение на удаление
        /// Если получен положительный ответ, генерирует соответствующее событие представления
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
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

        /// <summary>
        /// Получение выбранного результата тестирования из списка
        /// </summary>
        /// <returns>Выбранный результат тестирования</returns>
        private Tuple<int, string, string, DateTime, int, decimal> GetSelectedTestResult() =>
            ((Tuple<int, string, string, DateTime, int, decimal>)TestResultsListView.SelectedItem);

        #region ITestResultsView

        /// <summary>
        /// Событие запроса удаления результата тестирования
        /// </summary>
        public event Action RemoveTestResult;

        /// <summary>
        /// Событие запроса удаления всех результатов тестирования
        /// </summary>
        public event Action RemoveAllTestResults;

        /// <summary>
        /// Идентификатор результата тестирования для удаления
        /// </summary>
        public int TestResultToRemoveId =>
            GetSelectedTestResult().Item1;

        /// <summary>
        /// Показ результатов тестирования
        /// </summary>
        /// <param name="results">
        /// Результаты тестирования:
        /// - первый элемент кортежа - идентификатор результата тестирования;
        /// - второй элемент кортежа - наименование учебного предмета пройденного теста;
        /// - третий элемент кортежа - наименование пройденного теста;
        /// - четвертый элемент кортежа - дата и время начала тестирования;
        /// - пятый элемент кортежа - время в секундах, затраченное на прохождения теста;
        /// - шестой элемент кортежа - процент правильных ответов
        /// </param>
        public void ShowTestResults(IReadOnlyList<Tuple<int, string, string, DateTime, int, decimal>> results)
        {
            var isEmpty = !results.Any();
            Device.BeginInvokeOnMainThread(() =>
            {
                EmptyTestResultsLabel.IsVisible = isEmpty;
                TestResultsListView.IsVisible = !isEmpty;
                TestResultsListView.ItemsSource = results;
            });
        }

        /// <summary>
        /// Отображение страницы
        /// </summary>
        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage.Navigation.PushAsync(this));

        #endregion
    }
}