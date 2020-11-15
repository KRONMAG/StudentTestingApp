using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница главного меню приложения
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage, IMainView
    {
        /// <summary>
        /// Инициализация страницы
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Обработчик нажатия на пункт показа списка учебных предметов
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void SubjectsViewClicked(object sender, EventArgs e) =>
            GoToSubjectsView?.Invoke();

        /// <summary>
        /// Обработчик нажатия на пункт показа результатов тестирования
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void TestResultsViewClicked(object sender, EventArgs e) =>
            GoToTestResultsView?.Invoke();

        private void SettingsViewClicked(object sender, EventArgs e) =>
            GoToSettingsView?.Invoke();

        #region IMainView

        /// <summary>
        /// Событие выбора пункта перехода к представлению списка учебных предметов
        /// </summary>
        public event Action GoToSubjectsView;

        /// <summary>
        /// Событие выбора пункта перехода к представлению списка результатов тестирования
        /// </summary>
        public event Action GoToTestResultsView;

        public event Action GoToMarksView;

        public event Action GoToSettingsView;

        /// <summary>
        /// Показ представления
        /// </summary>
        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage = new NavigationPage(this)
                {
                    BarBackgroundColor = Color.FromHex("212121")
                });

        #endregion
    }
}