using System;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Представление настроек приложения
    /// </summary>
    public interface ISettingsView : IView
    {
        /// <summary>
        /// Событие запроса обновления базы данных тестов
        /// </summary>
        event Action UpdateTests;

        /// <summary>
        /// Событие запроса входа пользователя в систему Дневник
        /// Первый параметр события - логин пользователя, второй параметр - его пароль
        /// </summary>
        event Action<string, string> TryLogInToDnevnik;

        /// <summary>
        /// Событие запроса выхода пользователя из системы Дневник
        /// </summary>
        event Action LogOutFromDnevnik;

        /// <summary>
        /// Очистка введенного пароля
        /// </summary>
        void ClearPassword();

        /// <summary>
        /// Очистка введенного пароля
        /// </summary>
        void ClearLogin();

        /// <summary>
        /// Очистика даты истечения авторизации
        /// </summary>
        void ClearExpirationDate();

        /// <summary>
        /// Отображение логина пользователя в поле его ввода
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        void ShowLogin(string login);

        /// <summary>
        /// Показ даты истечения авторизации в Дневнике
        /// </summary>
        /// <param name="date">Дата, до которой действительна авторизация</param>
        void ShowExpirationDate(DateTime date);
    }
}