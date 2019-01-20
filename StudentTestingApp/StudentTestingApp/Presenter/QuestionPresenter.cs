using System;
using System.Collections.Generic;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class QuestionPresenter : IPresenter<Question>
    {
        private readonly IQuestionView _questionView;
        private readonly IReadOnlyRepository<Answer> _answerRepository;
        private readonly IDictionary<int, Answer> _selectedAnswers;
        private int _rightAnswerNumber;
        private bool _viewShown;

        public IQuestionView QuestionView => _questionView;

        public bool RightAnswerSelected =>
            _selectedAnswers.Count(pair => pair.Value.Right) == _rightAnswerNumber &&
            _selectedAnswers.Count == _rightAnswerNumber;

        public QuestionPresenter(IQuestionView questionView, IReadOnlyRepository<Answer> answerRepository)
        {
            _questionView = questionView;
            _answerRepository = answerRepository;
            _selectedAnswers = new Dictionary<int, Answer>();
            _rightAnswerNumber = 0;
            _viewShown = false;
        }

        ~QuestionPresenter()
        {
            if (_viewShown)
            {
                _questionView.Close();
            }
        }

        private void AnswerSelected()
        {
            var selectedAnswerId = _questionView.SelectedAnswerId;
            var answerAlreadySelected = _selectedAnswers.Keys.Contains(selectedAnswerId);
            if (!answerAlreadySelected)
            {
                if (_rightAnswerNumber == 1)
                {
                    _selectedAnswers.Clear();
                }

                var selectedAnswer = _answerRepository.GetItem(selectedAnswerId);
                _selectedAnswers.Add(selectedAnswer.Id, selectedAnswer);
            }
        }

        private void AnswerUnselected()
        {
            var unselectedAnswerId = _questionView.UnselectedAnswerId;
            _selectedAnswers.Remove(unselectedAnswerId);
        }

        public void Run(Question parameter)
        {
            var random = new Random();
            var answers = _answerRepository.GetItems(answer => answer.QuestionId == parameter.Id)
                .OrderBy(answer => random.Next()).ToList();
            var rightAnswerNumber = answers.Count(answer => answer.Right);
            _rightAnswerNumber = rightAnswerNumber;
            _questionView.SetQuestion(parameter.Text, parameter.Image);
            _questionView.SetSelectionMode(rightAnswerNumber == 1 ? SelectionMode.Single : SelectionMode.Multiply);
            _questionView.SetAnswers(answers.Select(answer => new Tuple<int, string>(answer.Id, answer.Text)));
            _questionView.AnswerSelected += AnswerSelected;
            _questionView.AnswerUnselected += AnswerUnselected;
            _questionView.Show();
            _viewShown = true;
        }
    }
}