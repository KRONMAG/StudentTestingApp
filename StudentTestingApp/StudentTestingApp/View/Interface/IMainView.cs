using System;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Представление главного меню приложения
    /// </summary>
    public interface IMainView : IView
    {
        /// <summary>
        /// Событие выбора пункта перехода к представлению списка учебных предметов
        /// </summary>
        event Action GoToSubjectsView;

        /// <summary>
        /// Событие выбора пункта перехода к представлению списка результатов тестирования
        /// </summary>
        event Action GoToTestResultsView;

        event Action GoToMarksView;

        event Action GoToSettingsView;
    }
}