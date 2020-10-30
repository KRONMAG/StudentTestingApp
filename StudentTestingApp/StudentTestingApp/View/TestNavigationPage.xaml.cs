using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница навигации по вопросам теста
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestNavigationPage : TabbedPage, ITestNavigationView
    {
        /// <summary>
        /// Инициализация страницы
        /// </summary>
        public TestNavigationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        /// <summary>
        /// Обработчик нажатия кнопки завершения тестирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void FinishTestClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Выход", "Вы действительно хотите завершить тестирование?", "Да", "Нет"))
                FinishTest?.Invoke();
        }

        #region ITestNavigationView

        /// <summary>
        /// Событие запроса завершения тестирования
        /// </summary>
        public event Action FinishTest;

        /// <summary>
        /// Показ сраниц с вопросами теста
        /// </summary>
        /// <param name="views">Список страниц с вопросами теста</param>
        public void ShowQuestionViews(IReadOnlyList<IQuestionView> views)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Children.Clear();
                views.ToList().ForEach(questionView =>
                {
                    Page page = (Page)questionView;
                    Children.Add(page);
                    page.Title = Children.Count.ToString();
                });
            });
        }

        /// <summary>
        /// Показ оставшегося времени тестирования
        /// </summary>
        /// <param name="seconds">Оставшееся время тестирования в секундах</param>
        public void ShowRemainingTime(int seconds)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ClockToolbarItem.IconImageSource = "clock.png";
                RemainingSecondsToolbarItem.Text = seconds.ToString();
            });
        }

        /// <summary>
        /// Показ страницы
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