using System;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class TestStartPresenter : IPresenter
    {
        private ITestStartView testStartView;
        private Test test;

        public TestStartPresenter(ITestStartView testStartView, Test test)
        {
            this.testStartView = testStartView;
            this.test = test;
            testStartView.OnStartTest += startTest;
        }

        public void Run()
        {
            testStartView.SetTest(test);
            testStartView.Show();
        }

        private void startTest()
        {
            if (string.IsNullOrEmpty(testStartView.StudentName) || string.IsNullOrWhiteSpace(testStartView.StudentName))
                testStartView.ShowError("Введите свои фамилию, имя, отчество для начала тестирования");
            else
                new TestNavigationPresenter(App.Container.Resolve<ITestNavigationView>(), test).Run();
        }
    }
}