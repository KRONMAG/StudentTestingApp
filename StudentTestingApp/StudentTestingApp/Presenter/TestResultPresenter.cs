using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class TestResultPresenter : IPresenter<TestResult>
    {
        private readonly ITestResultView _testResultView;

        public TestResultPresenter(ITestResultView testResultView)
        {
            _testResultView = testResultView;
        }

        public void Run(TestResult parameter)
        {
            var elapsedTime = (int) (parameter.EndDate - parameter.StartDate).TotalSeconds;
            var result = parameter.Result;
            _testResultView.Show();
            _testResultView.SetTestResult(elapsedTime, result);
        }
    }
}