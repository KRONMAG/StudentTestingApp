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
        /// Параметр события - идентификатор выбранного теста
        /// </summary>
        event Action<int> SelectTest;

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