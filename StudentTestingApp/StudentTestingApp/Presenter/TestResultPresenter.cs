using StudentTestingApp.Model.Entity;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Common;

namespace StudentTestingApp.Presenter
{
    public class TestResultPresenter : BasePresenter<ITestResultView, TestResult>
    {
        public TestResultPresenter(
            ApplicationController controller,
            ITestResultView view) :
            base(controller, view) =>
            view.DelayedResultUploadingSelected += GoToMainViewSelected;

        private void GoToMainViewSelected() =>
            controller.CreatePresenter<MainPresenter>().Run();

        public override void Run(TestResult testResult)
        {
            view.SetTestResult
            (
                (int)(testResult.EndDate - testResult.StartDate).TotalSeconds,
                testResult.Score
            );
            view.Show();
        }
    }
}