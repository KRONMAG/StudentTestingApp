using StudentTestingApp.Model.Entity;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Common;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// ������������� ������������� ���������� ������������
    /// </summary>
    public class TestResultPresenter : BasePresenter<ITestResultView, TestResult>
    {
        /// <summary>
        /// �������� ���������� ������
        /// </summary>
        /// <param name="controller">���������� ����������</param>
        /// <param name="view">������������� ���������� ������������</param>
        public TestResultPresenter(ApplicationController controller, ITestResultView view) :
            base(controller, view) =>
            view.GoToMainView += GoToMainView;

        /// <summary>
        /// ���������� ������� �������� � ������������� ���� ����������
        /// </summary>
        private void GoToMainView() =>
            controller.CreatePresenter<MainPresenter>().Run();

        /// <summary>
        /// ����� ���������� ������������, �������������
        /// </summary>
        /// <param name="testResult">��������� ������������</param>
        public override void Run(TestResult testResult)
        {
            view.ShowTestResult
            (
                (int)(testResult.EndDate - testResult.StartDate).TotalSeconds,
                testResult.Score
            );
            base.Run(testResult);
        }
    }
}