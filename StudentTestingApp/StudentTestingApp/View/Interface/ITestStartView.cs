using System;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Представление отображения описания выбранного теста
    /// </summary>
    public interface ITestStartView : IView
    {
        /// <summary>
        /// Событие запроса начала тестирования
        /// </summary>
        event Action StartTest;

        /// <summary>
        /// Показ описания теста
        /// </summary>
        /// <param name="name">Наименование теста</param>
        /// <param name="questionCount">Количество вопросов в тесте</param>
        /// <param name="duration">
        /// Продолжительность тестирования в секундах,
        /// значение null - продолжительность не ограничена
        /// </param>
        void ShowTestInfo(string name, int questionCount, int? duration);
    }
}