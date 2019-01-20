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
    public class TestNavigationPresenter : IPresenter<Tuple<Test, string>>
    {
        private readonly IReadOnlyRepository<Question> _questionRepository;
        private readonly ITestNavigationView _testNavigationView;
        private readonly ICollection<QuestionPresenter> _questionPresenters;
        private readonly TestResult _testResult;

        public TestNavigationPresenter(ITestNavigationView testNavigationView,
            IReadOnlyRepository<Question> questionRepository)
        {
            _testNavigationView = testNavigationView;
            _questionRepository = questionRepository;
            _questionPresenters = new Collection<QuestionPresenter>();
            _testResult = new TestResult();
        }

        public void Run(Tuple<Test, string> parameter)
        {
            var questionViews = new Collection<IQuestionView>();
            var test = parameter.Item1;
            var random = new Random();
            var questions = _questionRepository.GetItems(question => question.TestId == test.Id)
                .OrderBy(question => random.Next()).Take((test.QuestionCount));
            foreach (Question question in questions)
            {
                var questionPresenter = ApplicationController.Instance.CreatePresenter<QuestionPresenter, Question>();
                questionPresenter.Run(question);
                questionViews.Add(questionPresenter.QuestionView);
                _questionPresenters.Add(questionPresenter);
            }
            _testNavigationView.SetQuestionViews(questionViews);
            _testNavigationView.TestEnded += TestEnded;
            _testNavigationView.Show();
            if (test.Duration != null)
            {
                _testNavigationView.StartTimer((int)test.Duration);
            }
            var studentName = parameter.Item2;
            _testResult.TestId = test.Id;
            _testResult.StudentName = studentName;
            _testResult.StartDate = DateTime.Now;
        }

        private void TestEnded()
        {
            _testResult.EndDate = DateTime.Now;
            _testResult.Result =
                Math.Round(
                    _questionPresenters.Count(questionPresenter => questionPresenter.RightAnswerSelected) /
                    (_questionPresenters.Count * 1.0) * 100, 2);
            _testNavigationView.Close();
            ApplicationController.Instance.CreatePresenter<TestResultPresenter, TestResult>().Run(_testResult);
        }
    }
}