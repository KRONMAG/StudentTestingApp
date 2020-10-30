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

        /// <summary>
        /// Обработчик нажатия на пункт обновления тестов
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void UpdateTestsClicked(object sender, EventArgs e) =>
            UpdateTests?.Invoke();

        #region IMainView

        /// <summary>
        /// Событие выбора пункта перехода к представлению списка учебных предметов
        /// </summary>
        public event Action GoToSubjectsView;

        /// <summary>
        /// Событие выбора пункта перехода к представлению списка результатов тестирования
        /// </summary>
        public event Action GoToTestResultsView;

        /// <summary>
        /// Событие выбора пункта обновления базы данных тестов
        /// </summary>
        public event Action UpdateTests;

        /// <summary>
        /// Показ представления
        /// </summary>
        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage = new NavigationPage(this)
                {
                    BarBackgroundColor = Color.FromHex("212121")
                });

        /// <summary>
        /// Показ сообщения
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public void ShowMessage(string message) =>
            Device.BeginInvokeOnMainThread(() =>
                DisplayAlert(string.Empty, message, "Назад"));

        #endregion
    }
}