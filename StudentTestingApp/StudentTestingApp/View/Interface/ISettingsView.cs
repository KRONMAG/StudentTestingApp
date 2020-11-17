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
        /// </summary>
        event Action TryLogInToDnevnik;

        /// <summary>
        /// Событие запроса выхода пользователя из системы Дневник
        /// </summary>
        event Action LogOutFromDnevnik;

        /// <summary>
        /// Логин пользователя
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// Показ даты истечения авторизации в Дневнике
        /// </summary>
        /// <param name="date">Дата, до которой действительна авторизация</param>
        void ShowExpirationDate(DateTime? date = null);
    }
}