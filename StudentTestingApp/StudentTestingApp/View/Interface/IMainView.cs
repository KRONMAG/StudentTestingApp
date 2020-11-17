using System;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Представление главного меню приложения
    /// </summary>
    public interface IMainView : IView
    {
        /// <summary>
        /// Событие запроса перехода к представлению списка учебных предметов
        /// </summary>
        event Action GoToSubjectsView;

        /// <summary>
        /// Событие запроса перехода к представлению списка результатов тестирования
        /// </summary>
        event Action GoToTestResultsView;

        /// <summary>
        /// Событие запроса перехода к представлению настроек приложения
        /// </summary>
        event Action GoToSettingsView;
    }
}