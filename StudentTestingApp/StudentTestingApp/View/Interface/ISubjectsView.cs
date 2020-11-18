using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Представление списка учебных предметов
    /// </summary>
    public interface ISubjectsView : IView
    {
        /// <summary>
        /// Событие выбора учебного предмета из списка
        /// Параметр события - идентификатор выбранного предмета
        /// </summary>
        event Action<int> SelectSubject;

        /// <summary>
        /// Показ списка учебных предметов
        /// </summary>
        /// <param name="subjects">
        /// Список учебных предметов
        /// - первый элемент кортежа - идентификатор теста;
        /// - второй элемент кортежа - наименование учебного предмета
        /// </param>
        void ShowSubjects(IReadOnlyList<Tuple<int, string>> subjects);
    }
}