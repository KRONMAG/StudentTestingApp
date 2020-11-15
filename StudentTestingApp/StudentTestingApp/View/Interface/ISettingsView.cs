using System;

namespace StudentTestingApp.View.Interface
{
    public interface ISettingsView : IView
    {
        /// <summary>
        /// Событие выбора пункта обновления базы данных тестов
        /// </summary>
        event Action UpdateTests;

        event Action TryLogInToDnevnik;

        event Action LogOutFromDnevnik;

        string Login { get; set; }

        string Password { get; set; }

        void ShowExpirationDate(DateTime? date = null);
    }
}