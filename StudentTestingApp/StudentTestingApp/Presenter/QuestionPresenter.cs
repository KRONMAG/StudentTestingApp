﻿using System;
using System.Collections.Generic;
using System.Linq;
using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления отображения вопроса теста
    /// </summary>
    public class QuestionPresenter : BasePresenter<IQuestionView, Question>
    {
        /// <summary>
        /// Хранилище вариантов ответов на вопросы
        /// </summary>
        private readonly ReadOnlyRepository<Answer> _repository;

        /// <summary>
        /// Выбранные варианты ответа
        /// </summary>
        private readonly Dictionary<int, Answer> _selectedAnswers;

        /// <summary>
        /// Количество правильных вариантов ответа на вопрос среди выбранных
        /// </summary>
        private int _rightAnswersCount;

        /// <summary>
        /// Представление вопроса теста
        /// </summary>
        public IQuestionView View =>
            view;

        /// <summary>
        /// Выбраны ли верные варианты ответа на вопрос
        /// </summary>
        public bool AreRightAnswersSelected =>
            _selectedAnswers.Values.Count == _rightAnswersCount &&
            _selectedAnswers.Count(pair => pair.Value.Right) == _rightAnswersCount;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представление вопроса теста</param>
        /// <param name="repository">Хранилище вариантов ответов на вопросы</param>
        public QuestionPresenter
            (ApplicationController controller,
            IQuestionView view,
            ReadOnlyRepository<Answer> repository) :
            base(controller, view)
        {
            _repository = repository;
            _selectedAnswers = new Dictionary<int, Answer>();
            _rightAnswersCount = 0;
            view.SelectAnswer += SelectAnswer;
            view.UnselectAnswer += UnselectAnswer;
        }

        /// <summary>
        /// Обработчик выбора варианта ответа
        /// </summary>
        /// <param name="selectedAnswerId">Идентификатор выбранного варианта ответа</param>
        private void SelectAnswer(int selectedAnswerId)
        {
            if (!_selectedAnswers.Keys.Contains(selectedAnswerId))
            {
                if (_rightAnswersCount == 1)
                    _selectedAnswers.Clear();
                var selectedAnswer = _repository
                    .Get()
                    .First(answer => answer.Id == selectedAnswerId);
                _selectedAnswers.Add(selectedAnswer.Id, selectedAnswer);
            }
        }

        /// <summary>
        /// Обработчик отмены выбора варианта ответа
        /// </summary>
        /// <param name="unselectedAnswerId">Идентификатор варианта ответа для отмены</param>
        private void UnselectAnswer(int unselectedAnswerId) =>
            _selectedAnswers.Remove(unselectedAnswerId);

        /// <summary>
        /// Показ вопроса, вариантов ответов, представления
        /// </summary>
        /// <param name="question">Вопрос, отображаемый в представлении</param>
        public override void Run(Question question)
        {
            _selectedAnswers.Clear();
            var random = new Random();
            var answers = _repository
                .Get()
                .Where(answer => answer.QuestionId == question.Id)
                .OrderBy(answer => random.Next())
                .ToList();
            _rightAnswersCount = answers.Count(answer => answer.Right);
            view.SetSelectionMode
            (
                _rightAnswersCount == 1 ? SelectionMode.Single : SelectionMode.Multiply
            );
            view.ShowQuestion(question.Text, question.Image);
            view.ShowAnswers
            (
                answers
                    .Select(answer => new Tuple<int, string>(answer.Id, answer.Text))
                    .ToList()
            );
            base.Run(question);
        }
    }
}