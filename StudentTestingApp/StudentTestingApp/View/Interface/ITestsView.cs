using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Представление списка тестов выбранного учебного предмета
    /// </summary>
    public interface ITestsView : IView
    {
        /// <summary>
        /// Событие выбора теста из списка
        /// </summary>
        event Action SelectTest;

        /// <summary>
        /// Идентификатор выбранного теста
        /// </summary>
        int SelectedTestId { get; }

        /// <summary>
        /// Показ списка тестов
        /// </summary>
        /// <param name="tests">
        /// Список тестов:
        /// - первый элемент кортежа - идентификатор теста;
        /// - второй элемент кортежа - наименование теста
        /// </param>
        void ShowTests(IReadOnlyList<Tuple<int, string>> tests);
    }
}