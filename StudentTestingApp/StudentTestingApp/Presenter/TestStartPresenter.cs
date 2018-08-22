using System;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class TestStartPresenter : IPresenter
    {
        private IParentView parentView;
        private ITestStartView testStartView;
        private Test test;

        public TestStartPresenter(IParentView parentView, ITestStartView testStartView, Test test)
        {
            this.parentView = parentView;
            this.testStartView = testStartView;
            this.test = test;
            testStartView.OnStartTest += startTest;
        }

        public void Run()
        {
            testStartView.Show(parentView);
            testStartView.SetTest(test);
        }

        private void startTest()
        {
            if (string.IsNullOrEmpty(testStartView.StudentName) || string.IsNullOrWhiteSpace(testStartView.StudentName))
                testStartView.ShowError("Введите свои фамилию, имя, отчество для начала тестирования");
            else
            {
                testStartView.Close();
                new TestNavigationPresenter(
                    parentView,
                    App.Container.Resolve<ITestNavigationView>(),
                    test).Run();
            }
        }
    }
}