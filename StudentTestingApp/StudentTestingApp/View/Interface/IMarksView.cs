﻿using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Представление статистики по количеству оценок, полученных обучающимся
    /// </summary>
    public interface IMarksView : IView
    {
        /// <summary>
        /// Отображение данных о количестве полученных оценок за каждый учебный год
        /// </summary>
        /// <param name="marks">
        /// Список с количеством полученных оценок за каждый год:
        /// - первый элемент кортежа - год начала учебного года;
        /// - второй элемент кортежа - год окончания учебного года;
        /// - третий элемент кортежа - количество полученных двоек;
        /// - четвертый элемент кортежа - количество полученных троек;
        /// - пятый элемент кортежа - количество полученных четверок;
        /// - шестой элемент кортежа - количество полученных пятерок
        /// </param>
        void ShowMarksЫStatistics(IReadOnlyList<Tuple<int, int, int, int, int, int>> marksStatistics);
    }
}