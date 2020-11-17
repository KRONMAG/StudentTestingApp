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
        /// Обработчик нажатия кнопки перехода к странице с учебными предметами
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void GoToSubjectsPageClicked(object sender, EventArgs e) =>
            GoToSubjectsView?.Invoke();

        /// <summary>
        /// Обработчик нажатия кнопки перехода к странице с результатами тестирования
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void GoToTestResultsPageClicked(object sender, EventArgs e) =>
            GoToTestResultsView?.Invoke();

        /// <summary>
        /// Обработчик нажатия кнопки перехода к странице настроек приложения
        /// </summary>
        /// <param name="sender">Параметры события</param>
        /// <param name="e">Источник события</param>
        private void GoToSettingsPageClicked(object sender, EventArgs e) =>
            GoToSettingsView?.Invoke();

        #region IMainView

        /// <summary>
        /// Событие запроса перехода к представлению списка учебных предметов
        /// </summary>
        public event Action GoToSubjectsView;

        /// <summary>
        /// Событие запроса перехода к представлению списка результатов тестирования
        /// </summary>
        public event Action GoToTestResultsView;

        /// <summary>
        /// Событие запроса перехода к представлению настроек приложения
        /// </summary>
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