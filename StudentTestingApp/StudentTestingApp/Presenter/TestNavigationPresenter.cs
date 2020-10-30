using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления навигации по вопросам теста
    /// </summary>
    public class TestNavigationPresenter : BasePresenter<ITestNavigationView, Test>
    {
        /// <summary>
        /// Хранилище учебных предметов
        /// </summary>
        private readonly IReadOnlyRepository<Subject> _subjectsRepository;

        /// <summary>
        /// Хранилище вопросов тестов
        /// </summary>
        private readonly IReadOnlyRepository<Question> _questionsRepository;

        /// <summary>
        /// Хранилище результатов тестирования
        /// </summary>
        private readonly IRepository<TestResult> _testResultsRepository;

        /// <summary>
        /// Представители представлений вопросов теста
        /// </summary>
        private readonly List<QuestionPresenter> _presenters;

        /// <summary>
        /// Результат тестирования
        /// </summary>
        private TestResult _testResult;

        /// <summary>
        /// Таймер отсчета оставшегося времени тестирования
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представление навигации во вопросам теста</param>
        /// <param name="subjectsRepository">Хранилище учебных предметов</param>
        /// <param name="questionsRepository">Хранилище вопросов тестов</param>
        /// <param name="testResultsRepository">Хранилище результатов тестирования</param>
        public TestNavigationPresenter
            (ApplicationController controller,
            ITestNavigationView view,
            IReadOnlyRepository<Subject> subjectsRepository,
            IReadOnlyRepository<Question> questionsRepository,
            IRepository<TestResult> testResultsRepository) :
            base(controller, view)
        {
            _subjectsRepository = subjectsRepository;
            _questionsRepository = questionsRepository;
            _testResultsRepository = testResultsRepository;
            _presenters = new List<QuestionPresenter>();
            view.FinishTest += FinishTest;
        }

        /// <summary>
        /// Обработчик запроса завершения тестирования
        /// </summary>
        private void FinishTest()
        {
            _timer?.Dispose();
            _testResult.EndDate = DateTime.Now;
            _testResult.Score = Math.Round
            (
                Convert.ToDecimal
                (
                    _presenters.Count(questionPresenter => questionPresenter.AreRightAnswersSelected) /
                    (_presenters.Count * 1.0) * 100
                ),
                2
            );
            _testResultsRepository.Add(_testResult);
            controller.CreatePresenter<TestResultPresenter, TestResult>().Run(_testResult);
        }

        /// <summary>
        /// Формирование случайной выборки вопросов выбранного теста,
        /// добавление представлений вопросов в представление навигации по вопросам теста,
        /// установка таймера отсчета оставшегося времени тестирования,
        /// показ представления
        /// </summary>
        /// <param name="test">Выбранный тест</param>
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
            view.ShowQuestionViews(views);
            if (test.Duration != null)
            {
                var remainingTime = test.Duration.Value;
                view.ShowRemainingTime(remainingTime);
                _timer = new Timer(state =>
                {
                    if ((remainingTime -= 1) > 0)
                        view.ShowRemainingTime(remainingTime);
                    else
                        FinishTest();
                }, null, 1000, 1000);
            }
            _testResult = new TestResult();
            _testResult.SubjectName = _subjectsRepository
                .Get(subject => subject.Id == test.Id)
                .First()
                .Name;
            _testResult.TestName = test.Name;
            _testResult.StartDate = DateTime.Now;
            base.Run(test);
        }
    }
}