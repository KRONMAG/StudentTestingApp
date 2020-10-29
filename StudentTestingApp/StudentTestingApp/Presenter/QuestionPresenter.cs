using System;
using System.Collections.Generic;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class QuestionPresenter : BasePresenter<IQuestionView, Question>
    {
        private readonly IReadOnlyRepository<Answer> _repository;
        private readonly IDictionary<int, Answer> _selectedAnswers;
        private int _rightAnswersCount;

        public IQuestionView View =>
            view;

        public bool RightAnswerSelected =>
            _selectedAnswers.Values.Count == _rightAnswersCount &&
            _selectedAnswers.Count(pair => pair.Value.Right) == _rightAnswersCount;

        public QuestionPresenter
            (ApplicationController controller,
            IQuestionView view,
            IReadOnlyRepository<Answer> repository) :
            base(controller, view)
        {
            _repository = repository;
            _selectedAnswers = new Dictionary<int, Answer>();
            _rightAnswersCount = 0;
            view.AnswerSelected += AnswerSelected;
            view.AnswerUnselected += AnswerUnselected;
        }

        private void AnswerSelected()
        {
            if (!_selectedAnswers.Keys.Contains(view.SelectedAnswerId))
            {
                if (_rightAnswersCount == 1)
                    _selectedAnswers.Clear();
                var selectedAnswer = _repository
                    .Get()
                    .First(answer => answer.Id == view.SelectedAnswerId);
                _selectedAnswers.Add(selectedAnswer.Id, selectedAnswer);
            }
        }
        
        private void AnswerUnselected() =>
            _selectedAnswers.Remove(view.UnselectedAnswerId);

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
            view.SetQuestion(question.Text, question.Image);
            view.SetSelectionMode
            (
                _rightAnswersCount == 1 ? SelectionMode.Single : SelectionMode.Multiply
            );
            view.SetAnswers
            (
                answers
                    .Select(answer => new Tuple<int, string>(answer.Id, answer.Text))
                    .ToList()
            );
            view.Show();
        }
    }
}