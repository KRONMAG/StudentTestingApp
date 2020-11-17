using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Представление списка результатов тестирования
    /// </summary>
    public interface ITestResultsView : IView
    {
        /// <summary>
        /// Событие запроса распространения результата тестирования в сети Дневник
        /// Параметр - идентификатор результата, которым надо поделиться
        /// </summary>
        event Action<int> ShareTestResult;

        /// <summary>
        /// Событие запроса удаления результата тестирования
        /// Параметр - идентификатор удаляемого результата
        /// </summary>
        event Action<int> RemoveTestResult;

        /// <summary>
        /// Событие запроса удаления всех результатов тестирования
        /// </summary>
        event Action RemoveAllTestResults;

        /// <summary>
        /// Показ результатов тестирования
        /// </summary>
        /// <param name="results">
        /// Список результатов тестирования
        /// - первый элемент кортежа - идентификатор результата тестирования;
        /// - второй элемент кортежа - наименование учебного предмета пройденного теста;
        /// - третий элемент кортежа - наименование пройденного теста;
        /// - четвертый элемент кортежа - дата и время начала тестирования;
        /// - пятый элемент кортежа - время в секундах, затраченное на прохождения теста;
        /// - шестой элемент кортежа - процент правильных ответов
        /// </param>
        void ShowTestResults(IReadOnlyList<Tuple<int, string, string, DateTime, int, decimal>> results);
    }
}