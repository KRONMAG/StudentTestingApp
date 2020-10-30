using System;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления списка результатов тестирования
    /// </summary>
    public class TestResultsPresenter : BasePresenter<ITestResultsView>
    {
        /// <summary>
        /// Хранилище результатов тестирования
        /// </summary>
        private IRepository<TestResult> _repository;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представление списка результатов тестирования</param>
        /// <param name="repository">Хранилище результатов тестирования</param>
        public TestResultsPresenter
            (ApplicationController controller,
            ITestResultsView view,
            IRepository<TestResult> repository) :
            base(controller, view)
        {
            _repository = repository;
            view.RemoveTestResult += RemoveTestResult;
            view.RemoveAllTestResults += RemoveAllTestResults;
        }

        /// <summary>
        /// Обработчик запроса удаления выбранного результата тестирования
        /// </summary>
        private void RemoveTestResult()
        {
            _repository.Remove(view.TestResultToRemoveId);
            LoadTestResults();
        }

        /// <summary>
        /// Обработчик запроса удаления всех результатов тестирования
        /// </summary>
        private void RemoveAllTestResults()
        {
            _repository.Clear();
            LoadTestResults();
        }

        /// <summary>
        /// Загрузка списка результатов тестирования в представление
        /// </summary>
        private void LoadTestResults()
        {
            view.ShowTestResults
            (
                _repository
                    .Get()
                    .OrderByDescending(result => result.StartDate)
                    .Select
                    (
                        result => new Tuple<int, string, string, DateTime, int, decimal>
                        (
                            result.Id,
                            result.SubjectName,
                            result.TestName,
                            result.StartDate,
                            (int)(result.EndDate - result.StartDate).TotalSeconds,
                            result.Score
                        )
                     )
                    .ToList()
             );
        }

        /// <summary>
        /// Показ результатов тестирования, представления
        /// </summary>
        public override void Run()
        {
            LoadTestResults();
            base.Run();
        }
    }
}