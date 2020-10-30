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
        /// </summary>
        event Action SelectSubject;

        /// <summary>
        /// Идентификатор выбранного учебного предмета
        /// </summary>
        int SelectedSubjectId { get; }

        /// <summary>
        /// Показ списка учебных предметов
        /// </summary>
        /// <param name="subjects">
        /// Список учебных предметов
        /// - первый элемент кортежа - идентификатор учебного теста;
        /// - второй элемент кортежа - наименование учебного предмета
        /// </param>
        void ShowSubjects(IReadOnlyList<Tuple<int, string>> subjects);
    }
}