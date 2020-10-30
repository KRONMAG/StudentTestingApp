using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница с описанием выбранного теста
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestStartPage : ContentPage, ITestStartView
    {
        /// <summary>
        /// Инициализация страницы
        /// </summary>
        public TestStartPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Обработчик нажатия кнопки начала тестирования
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void StartTestClicked(object sender, EventArgs e)
        {
            StartTestButton.IsEnabled = false;
            StartTest?.Invoke();
            StartTestButton.IsEnabled = true;
        }

        #region ITestStartView

        /// <summary>
        /// Событие запроса начала тестирования
        /// </summary>
        public event Action StartTest;

        /// <summary>
        /// Показ описания теста
        /// </summary>
        /// <param name="name">Наименование теста</param>
        /// <param name="questionCount">Количество вопросов в тесте</param>
        /// <param name="duration">
        /// Продолжительность тестирования в секундах,
        /// значение null - продолжительность не ограничена
        /// </param>
        public void ShowTestInfo(string name, int questionCount, int? duration)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                NameLabel.Text = name;
                QuestionCountLabel.Text = $"Количество вопросов: {questionCount}";
                DurationLabel.Text = duration == null
                    ? "Продолжительность не ограничена"
                    : $"Продолжительность: {duration} сек.";
            });
        }

        /// <summary>
        /// Показ сообщения
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public void ShowMessage(string message) =>
            Device.BeginInvokeOnMainThread(() =>
                DisplayAlert(string.Empty, message, "Назад"));

        /// <summary>
        /// Показ страницы
        /// </summary>
        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage.Navigation.PushAsync(this));

        #endregion
    }
}