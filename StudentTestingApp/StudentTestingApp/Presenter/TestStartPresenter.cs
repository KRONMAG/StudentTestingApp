using System;
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
            _testStartView.TestStarted += TestStarted;
            _testStartView.Show();
        }

        private void TestStarted()
        {
            var studentName = _testStartView.StudentName;
            var studentNameIncorrect = string.IsNullOrEmpty(studentName) ||
                                     string.IsNullOrWhiteSpace(studentName);
            if (studentNameIncorrect)
            {
                _testStartView.ShowError("Введите свои фамилию, имя, отчество для начала тестирования");
            }
            else
            {
                _testStartView.Close();
                ApplicationController.Instance.CreatePresenter<TestNavigationPresenter, Tuple<Test, string>>()
                    .Run(new Tuple<Test, string>(_test, studentName));
            }
        }
    }
}