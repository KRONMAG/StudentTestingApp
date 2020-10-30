using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Представление навигации по вопросам теста
    /// </summary>
    public interface ITestNavigationView : IView
    {
        /// <summary>
        /// Событие запроса завершения тестирования
        /// </summary>
        event Action FinishTest;

        /// <summary>
        /// Показ представлений вопросов теста
        /// </summary>
        /// <param name="questionViews">Представления вопросов теста</param>
        void ShowQuestionViews(IReadOnlyList<IQuestionView> views);

        /// <summary>
        /// Показ оставшегося времени тестирования
        /// Метод не используется, если время прохождения теста не ограничено
        /// </summary>
        /// <param name="seconds">Оставшееся время тестирования в секундах</param>
        void ShowRemainingTime(int seconds);
    }
}