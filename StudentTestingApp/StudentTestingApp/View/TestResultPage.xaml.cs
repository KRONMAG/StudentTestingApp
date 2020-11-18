using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница отображения результата тестирования
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestResultPage : ContentPage, ITestResultView
    {
        /// <summary>
        /// Инициализация страницы
        /// </summary>
        public TestResultPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Обработчик нажатия кнопки перехода на страницу с главным меню приложения
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void GoToMainPageClicked(object sender, EventArgs e)
        {
            GoToMainPageButton.IsEnabled = false;
            GoToMainView?.Invoke();
            GoToMainPageButton.IsEnabled = true;
        }

        #region ITestResultView

        /// <summary>
        /// Событие запроса перехода к представлению главного меню приложения
        /// </summary>
        public event Action GoToMainView;

        /// <summary>
        /// Показ результата тестирования
        /// </summary>
        /// <param name="elapsedTime">Время прохождения теста в секундах</param>
        /// <param name="score">Процент правильных ответов</param>
        public void ShowTestResult(int elapsedTime, decimal score)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ElapsedTimeLabel.Text = $"Затраченное время: {elapsedTime} сек.";
                ScoreLabel.Text = $"{score}%";
                ScoreProgressBar.Progress = 0;
            });
            new Timer(state =>
            {
                if (Convert.ToDecimal(ScoreProgressBar.Progress * 100) < score)
                    Device.BeginInvokeOnMainThread(() => ScoreProgressBar.Progress += 0.01);
            }, null, 0, 20);
        }

        /// <summary>
        /// Показ страницы
        /// </summary>
        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage = new NavigationPage(this));

        #endregion
    }
}