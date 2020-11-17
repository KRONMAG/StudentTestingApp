using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница настроек приложения
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage, ISettingsView
    {
        /// <summary>
        /// Инициализация страницы
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Обработчик нажатия кнопки обновления базы данных тестов
        /// </summary>
        /// <param name="sender">Параметры события</param>
        /// <param name="e">Источник события</param>
        private void UpdateTestsClicked(object sender, EventArgs e) =>
            UpdateTests?.Invoke();

        /// <summary>
        /// Обработчик нажатия кнопки входа в систему Дневник
        /// </summary>
        /// <param name="sender">Параметры события</param>
        /// <param name="e">Источник события</param>
        private void LogInToDnevnikClicked(object sender, EventArgs e) =>
            TryLogInToDnevnik?.Invoke();

        /// <summary>
        /// Обработчик нажатия кнопки выхода из Дневника
        /// </summary>
        /// <param name="sender">Параметры события</param>
        /// <param name="e">Источник события</param>
        private void LogOutFromDnevnikClicked(object sender, EventArgs e) =>
            LogOutFromDnevnik?.Invoke();

        #region ISettingsView

        /// <summary>
        /// Событие запроса обновления базы данных тестов
        /// </summary>
        public event Action UpdateTests;

        /// <summary>
        /// Событие запроса входа в систему Дневник
        /// </summary>
        public event Action TryLogInToDnevnik;

        /// <summary>
        /// Событие запроса выхода из Дневника
        /// </summary>
        public event Action LogOutFromDnevnik;

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login
        {
            get => LoginEntry.Text;
            set => Device.BeginInvokeOnMainThread(() => LoginEntry.Text = value);
        }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password
        {
            get => PasswordEntry.Text;
            set => Device.BeginInvokeOnMainThread(() => PasswordEntry.Text = value);
        }

        /// <summary>
        /// Показ даты истечения авторизации в системе Дневник
        /// </summary>
        /// <param name="date">Дата истечения авторизации</param>
        public void ShowExpirationDate(DateTime? date = null) =>
            Device.BeginInvokeOnMainThread(() =>
            {
                if (date != null)
                    ExpirationDateLabel.Text = date.ToString();
                else
                    ExpirationDateLabel.Text = "вход не выполнен";
            });

        /// <summary>
        /// Показ представления
        /// </summary>
        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                Application.Current.MainPage.Navigation.PushAsync(this));

        #endregion
    }
}