using StudentTestingApp.Model.Entity;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;
using System.Linq;

namespace StudentTestingApp.Presenter
{
    public class TestResultPresenter : IPresenter<TestResult>
    {
        private readonly ITestResultView _testResultView;
        private readonly IRepository<TestResult> _testResultRepository;
        private TestResult _testResult;

        public TestResultPresenter(ITestResultView testResultView, IRepository<TestResult> testResultRepository)
        {
            _testResultView = testResultView;
            _testResultRepository = testResultRepository;
        }

        public void Run(TestResult parameter)
        {
            var elapsedTime = (int) (parameter.EndDate - parameter.StartDate).TotalSeconds;
            var result = parameter.Result;
            _testResult = parameter;
            _testResultView.DelayedResultUploadingSelected += DelayedResultUploadingSelected;
            _testResultView.Show();
            _testResultView.SetTestResult(elapsedTime, result);
        }

        private void DelayedResultUploadingSelected()
        {
            _testResultRepository.Add(_testResult);
            _testResultView.ShowMessage("Результат тестирования был сохранен локально");
            _testResultView.Close();
            ApplicationController.Instance.CreatePresenter<SubjectListPresenter>().Run();
        }
    }
}