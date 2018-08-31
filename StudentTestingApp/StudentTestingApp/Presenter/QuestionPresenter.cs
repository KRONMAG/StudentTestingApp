using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class QuestionPresenter : IPresenter<Tuple<TestNavigationPresenter.TestNavigationHelper, Question>>
    {
        private readonly IQuestionView _questionView;
        private readonly IReadOnlyRepository<Answer> _answerRepository;
        private readonly ICollection<Answer> _selectedAnswers;
        private int _rightAnswerNumber;

        public IQuestionView QuestionView => _questionView;

        public bool RightAnswerSelected =>
            _selectedAnswers.Count(selectedAnswer => selectedAnswer.Right) == _rightAnswerNumber;

        public QuestionPresenter(IQuestionView questionView, IReadOnlyRepository<Answer> answerRepository)
        {
            _questionView = questionView;
            _answerRepository = answerRepository;
            _selectedAnswers = new Collection<Answer>();
            _rightAnswerNumber = 0;
        }

        private void SelectAnswer()
        {
            var answerAlreadySelected =
                _selectedAnswers.Count(selectedAnswer => selectedAnswer.Id == _questionView.SelectedAnswerId) > 0;
            if (!answerAlreadySelected)
            {
                if (_rightAnswerNumber == 1)
                {
                    _selectedAnswers.Clear();
                }

                var selectedAnswer = _answerRepository.GetItem(_questionView.SelectedAnswerId);
                _selectedAnswers.Add(selectedAnswer);
            }
        }

        private void UnselectAnswer()
        {
            var unselectedAnswer = _answerRepository.GetItem(_questionView.UnselectedAnswerId);
            _selectedAnswers.Remove(unselectedAnswer);
        }

        public void Run(Tuple<TestNavigationPresenter.TestNavigationHelper, Question> parameter)
        {
            var testNavigationHelper = parameter.Item1;
            var question = parameter.Item2;
            var random = new Random();
            var answers = _answerRepository.GetItems(answer => answer.QuestionId == question.Id)
                .OrderBy(answer => random.Next());
            var answerNumber = answers.Count();
            if (answerNumber > 0 && answers.Count(answer => answer.Right) > 0)
            {
                _rightAnswerNumber = answers.Count(answer => answer.Right);
                _questionView.SetQuestion(question.Text, question.Image);
                _questionView.SetSelectionMode(answers.Count(answer => answer.Right) == 1
                    ? SelectionMode.Single
                    : SelectionMode.Multiply);
                _questionView.SetAnswers(answers.Select(answer => new Tuple<int, string>(answer.Id, answer.Text)));
                _questionView.OnSelectAnswer += SelectAnswer;
                _questionView.OnUnselectAnswer += UnselectAnswer;
                _questionView.Show();
                testNavigationHelper.AddQuestionView(_questionView);
            }
        }
    }
}