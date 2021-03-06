﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница со списком учебных предметов
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectsPage : ContentPage, ISubjectsView
    {
        /// <summary>
        /// Инициализация страницы
        /// </summary>
        public SubjectsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Обработчик нажатия на элемент списка учебных предметов
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void SubjectTapped(object sender, ItemTappedEventArgs e) =>
            SelectSubject?.Invoke();

        #region ISubjectsView

        /// <summary>
        /// Событие выбора учебного предмета из списка
        /// </summary>
        public event Action SelectSubject;

        /// <summary>
        /// Идентификатор выбранного учебного предмета
        /// </summary>
        public int SelectedSubjectId => ((Tuple<int, string>)SubjectsListView.SelectedItem).Item1;

        /// <summary>
        /// Показ списка учебных предметов
        /// </summary>
        /// <param name="subjects">
        /// Список учебных предметов
        /// - первый элемент кортежа - идентификатор учебного теста;
        /// - второй элемент кортежа - наименование учебного предмета
        /// </param>
        public void ShowSubjects(IReadOnlyList<Tuple<int, string>> subjects)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var isEmpty = !subjects.Any();
                EmptySubjectsLabel.IsVisible = isEmpty;
                SubjectsListView.IsVisible = !isEmpty;
                SubjectsListView.ItemsSource = new ObservableCollection<Tuple<int, string>>(subjects);
            });
        }

        /// <summary>
        /// Показ страницы
        /// </summary>
        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage.Navigation.PushAsync(this));

        #endregion
    }
}