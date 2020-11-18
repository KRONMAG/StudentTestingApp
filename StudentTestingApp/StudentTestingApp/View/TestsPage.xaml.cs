﻿using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница со списком тестов выбранного учебного предмета
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestsPage : ContentPage, ITestsView
    {
        /// <summary>
        /// Инициализация страницы
        /// </summary>
        public TestsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Обработчик нажатия на элемент списка тестов
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void TestTapped(object sender, ItemTappedEventArgs e) =>
            SelectTest?.Invoke(((Tuple<int, string>)TestsListView.SelectedItem).Item1);

        #region ITestsView

        /// <summary>
        /// Событие выбора теста из списка
        /// Параметр события - идентификатор выбранного теста
        /// </summary>
        public event Action<int> SelectTest;

        /// <summary>
        /// Показ списка тестов
        /// </summary>
        /// <param name="tests">
        /// Список тестов:
        /// - первый элемент кортежа - идентификатор теста;
        /// - второй элемент кортежа - наименование теста
        /// </param>
        public void ShowTests(IReadOnlyList<Tuple<int, string>> tests)
        {
            var isEmpty = !tests.Any();
            Device.BeginInvokeOnMainThread(() =>
            {
                EmptyTestsLabel.IsVisible = isEmpty;
                TestsListView.IsVisible = !isEmpty;
                TestsListView.ItemsSource = tests;
            });
        }

        /// <summary>
        /// Показ страницы
        /// </summary>
        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                App.Current.MainPage.Navigation.PushAsyncSingle(this));

        #endregion
    }
}