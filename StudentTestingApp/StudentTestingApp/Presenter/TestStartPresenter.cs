using System;
using System.Threading.Tasks;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Model;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class TestStartPresenter
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
            testStartView.Test = test;
            testStartView.Show(parentView);
        }

        private void startTest()
        {
            if (string.IsNullOrEmpty(testStartView.StudentName) || string.IsNullOrWhiteSpace(testStartView.StudentName))
                testStartView.ShowError("Введите свои фамилию, имя, отчество для начала тестирования");
            else new TestNavigationPresenter(parentView, App.Container.Resolve<ITestNavigationView>(), test);
        }
    }
}