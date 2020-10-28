﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class TestNavigationPresenter : BasePresenter<ITestNavigationView, Test>
    {
        private readonly IReadOnlyRepository<Question> _questionsRepository;
        private readonly IReadOnlyRepository<Subject> _subjectsRepository;
        private readonly IRepository<TestResult> _testResultsRepository;
        private readonly List<QuestionPresenter> _presenters;
        private TestResult _testResult;

        public TestNavigationPresenter
            (ApplicationController controller,
            ITestNavigationView view,
            IReadOnlyRepository<Question> questionsRepository,
            IReadOnlyRepository<Subject> subjectsRepository,
            IRepository<TestResult> testResultsRepository) :
            base(controller, view)
        {
            _questionsRepository = questionsRepository;
            _subjectsRepository = subjectsRepository;
            _testResultsRepository = testResultsRepository;
            _presenters = new List<QuestionPresenter>();
            view.TestEnded += TestEnded;
        }

        private void TestEnded()
        {
            _testResult.EndDate = DateTime.Now;
            _testResult.Score = Math.Round
            (
                _presenters.Count(questionPresenter => questionPresenter.RightAnswerSelected) /
                    (_presenters.Count * 1.0) * 100,
                2
            );
            _testResultsRepository.Add(_testResult);
            controller.CreatePresenter<TestResultPresenter, TestResult>().Run(_testResult);
        }

        public override void Run(Test test)
        {
            _presenters.Clear();
            var views = new Collection<IQuestionView>();
            var random = new Random();
            var questions = _questionsRepository
                .Get(question => question.TestId == test.Id)
                .OrderBy(question => random.Next())
                .Take(test.QuestionsCount);
            foreach (Question question in questions)
            {
                var presenter = controller.CreatePresenter<QuestionPresenter, Question>();
                presenter.Run(question);
                views.Add(presenter.View);
                _presenters.Add(presenter);
            }
            view.SetQuestionViews(views);
            if (test.Duration != null)
            {
                view.StartTimer((int)test.Duration);
            }
            _testResult = new TestResult();
            _testResult.SubjectName = _subjectsRepository
                .Get(subject => subject.Id == test.Id)
                .First()
                .Name;
            _testResult.TestName = test.Name;
            _testResult.StartDate = DateTime.Now;
            _testResult.IsSended = false;
            view.Show();
        }
    }
}