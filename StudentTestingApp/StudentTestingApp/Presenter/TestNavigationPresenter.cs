using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class TestNavigationPresenter : IPresenter<Test>
    {
        private readonly IReadOnlyRepository<Question> _questionRepository;
        private readonly ITestNavigationView _testNavigationView;
        private readonly ICollection<IQuestionView> QuestionViews;

        public TestNavigationPresenter(ITestNavigationView testNavigationView,
            IReadOnlyRepository<Question> questionRepository)
        {
            _testNavigationView = testNavigationView;
            _questionRepository = questionRepository;
            var questionViews = new ObservableCollection<IQuestionView>();
            questionViews.CollectionChanged += QuestionViewsChanged;
            QuestionViews = questionViews;
        }

        private void QuestionViewsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _testNavigationView.SetQuestionViews(QuestionViews);
        }

        public void Run(Test parameter)
        {
            var random = new Random();
            var testNavigationHelper = new TestNavigationHelper(this);
            var questions = _questionRepository.GetItems(question => question.TestId == parameter.Id)
                .OrderBy(question => random.Next()).Take((parameter.QuestionCount));
            foreach (Question question in questions)
            {
                ApplicationController.Instance.Run<QuestionPresenter, Tuple<TestNavigationHelper, Question>>(
                    new Tuple<TestNavigationHelper, Question>(testNavigationHelper, question));
            }

            _testNavigationView.Show();
            if (parameter.Duration != null)
            {
                _testNavigationView.StartTimer((int) parameter.Duration);
            }
        }

        public class TestNavigationHelper
        {
            private readonly TestNavigationPresenter _testNavigationPresenter;

            public TestNavigationHelper(TestNavigationPresenter testNavigationPresenter)
            {
                _testNavigationPresenter = testNavigationPresenter;
            }

            public void AddQuestionView(IQuestionView questionView)
            {
                _testNavigationPresenter.QuestionViews.Add(questionView);
            }
        }
    }
}