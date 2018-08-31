using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class TestStartPresenter : IPresenter<Test>
    {
        private readonly ITestStartView _testStartView;
        private Test _test;

        public TestStartPresenter(ITestStartView testStartView)
        {
            _testStartView = testStartView;
        }

        public void Run(Test parameter)
        {
            _test = parameter;
            _testStartView.SetTest(parameter.Name, parameter.QuestionCount, parameter.Duration);
            _testStartView.OnStartTest += StartTest;
            _testStartView.Show();
        }

        private void StartTest()
        {
            var studentNameCorrect = string.IsNullOrEmpty(_testStartView.StudentName) ||
                                     string.IsNullOrWhiteSpace(_testStartView.StudentName);
            if (studentNameCorrect)
            {
                _testStartView.ShowError("Введите свои фамилию, имя, отчество для начала тестирования");
            }
            else
            {
                _testStartView.Close();
                ApplicationController.Instance.Run<TestNavigationPresenter, Test>(_test);
            }
        }
    }
}