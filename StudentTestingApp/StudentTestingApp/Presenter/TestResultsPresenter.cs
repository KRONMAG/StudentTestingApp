using System;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class TestResultsPresenter : BasePresenter<ITestResultsView>
    {
        private IRepository<TestResult> _repository;

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

        private void RemoveTestResult()
        {
            _repository.Remove(view.TestResultToRemoveId);
            LoadTestResults();
        }

        private void RemoveAllTestResults()
        {
            _repository.Clear();
            LoadTestResults();
        }

        private void LoadTestResults()
        {
            view.SetTestResults
            (
                _repository
                    .Get()
                    .OrderByDescending(result => result.StartDate)
                    .Select
                    (
                        result => new Tuple<int, string, string, DateTime, int, double>
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

        public override void Run()
        {
            LoadTestResults();
            view.Show();
        }
    }
}