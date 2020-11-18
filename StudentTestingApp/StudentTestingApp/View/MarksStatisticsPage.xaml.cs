using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница отображения данных о полученных оценках
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MarksStatisticsPage : ContentPage, IMarksView
    {
        /// <summary>
        /// Инициализация страницы
        /// </summary>
        public MarksStatisticsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        #region IMarksView

        /// <summary>
        /// Отображение данных о количестве полученных оценок за каждый учебный год
        /// </summary>
        /// <param name="marks">
        /// Список с количеством полученных оценок за каждый год:
        /// - первый элемент кортежа - год начала учебного года;
        /// - второй элемент кортежа - год окончания учебного года;
        /// - третий элемент кортежа - количество полученных двоек;
        /// - четвертый элемент кортежа - количество полученных троек;
        /// - пятый элемент кортежа - количество полученных четверок;
        /// - шестой элемент кортежа - количество полученных пятерок
        /// </param>
        public void ShowMarksЫStatistics(IReadOnlyList<Tuple<int, int, int, int, int, int>> marks)
        {
            var isEmpty = !marks.Any();
            Device.BeginInvokeOnMainThread(() =>
            {
                EmptyMarksLabel.IsVisible = isEmpty;
                MarksDataGrid.IsVisible = !isEmpty;

                MarksDataGrid.ItemsSource = marks.Select(mark => new Tuple<string, int, int, int, int>
                    (
                        $"{mark.Item1}/{mark.Item2}",
                        mark.Item3,
                        mark.Item4,
                        mark.Item5,
                        mark.Item6
                    ));
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