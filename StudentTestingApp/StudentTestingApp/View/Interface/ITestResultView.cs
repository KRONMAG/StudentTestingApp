using System;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Представление результата тестирования
    /// </summary>
    public interface ITestResultView : IView
    {
        /// <summary>
        /// Событие запроса перехода к представлению главного меню приложения
        /// </summary>
        event Action GoToMainView;

        /// <summary>
        /// Показ результата тестирования
        /// </summary>
        /// <param name="elapsedTime">Время прохождения теста в секундах</param>
        /// <param name="score">Процент правильных ответов</param>
        void ShowTestResult(int elapsedTime, decimal score);
    }
}