﻿using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Представление вопроса теста
    /// </summary>
    public interface IQuestionView : IView
    {
        /// <summary>
        /// Событие выбора варианта ответа
        /// Параметр события - идентификатор выбранного варианта ответа
        /// </summary>
        event Action<int> SelectAnswer;

        /// <summary>
        /// Событие отмены выбора варианта ответа
        /// Параметр события - идентификатор варианта ответа для отмены
        /// </summary>
        event Action<int> UnselectAnswer;

        /// <summary>
        /// Установка режима выбора вариантов ответов
        /// </summary>
        /// <param name="selectionMode">Режим выбора ответов</param>
        void SetSelectionMode(SelectionMode selectionMode);

        /// <summary>
        /// Показ вопроса
        /// </summary>
        /// <param name="text">Текст вопроса</param>
        /// <param name="image">
        /// Изображение, поясняющее текст вопроса,
        /// значение null - изображение отсутствует
        /// </param>
        void ShowQuestion(string text, byte[] image = null);

        /// <summary>
        /// Показ вариантов ответа на вопрос
        /// </summary>
        /// <param name="answers">
        /// Варианты ответа на вопрос:
        /// - первый элемент кортежа - идентификатор варианта ответа;
        /// - второй элемент кортежа - текст варианта ответа
        /// </param>
        void ShowAnswers(IReadOnlyList<Tuple<int, string>> answers);
    }
}