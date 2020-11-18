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
            TryLogInToDnevnik?.Invoke(LoginEntry.Text, PasswordEntry.Text);

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
        /// Первый параметр события - логин пользователя, второй - его пароль
        /// </summary>
        public event Action<string, string> TryLogInToDnevnik;

        /// <summary>
        /// Событие запроса выхода из Дневника
        /// </summary>
        public event Action LogOutFromDnevnik;

        /// <summary>
        /// Очистка введенного пароля
        /// </summary>
        public void ClearPassword() =>
            Device.BeginInvokeOnMainThread(() =>
                PasswordEntry.Text = string.Empty);

        /// <summary>
        /// Очистка введенного логина
        /// </summary>
        public void ClearLogin() =>
            Device.BeginInvokeOnMainThread(() =>
                LoginEntry.Text = string.Empty);

        /// <summary>
        /// Очистка даты истечения авторизации
        /// </summary>
        public void ClearExpirationDate() =>
            Device.BeginInvokeOnMainThread(() =>
                ExpirationDateLabel.Text = "вход не выполнен");

        /// <summary>
        /// Показ логина
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        public void ShowLogin(string login) =>
            Device.BeginInvokeOnMainThread(() =>    
                LoginEntry.Text = login);

        /// <summary>
        /// Показ даты истечения авторизации в системе Дневник
        /// </summary>
        /// <param name="date">Дата истечения авторизации</param>
        public void ShowExpirationDate(DateTime date) =>
            Device.BeginInvokeOnMainThread(() =>
                ExpirationDateLabel.Text = date.ToString());

        /// <summary>
        /// Показ представления
        /// </summary>
        public void Show() =>
            Device.BeginInvokeOnMainThread(() =>
                App.Current.MainPage.Navigation.PushAsyncSingle(this));

        #endregion
    }
}