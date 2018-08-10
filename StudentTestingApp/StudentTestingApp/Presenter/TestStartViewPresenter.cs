using System;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Model;

namespace StudentTestingApp.Presenter
{
    class TestStartViewPresenter
    {
        private ITestStartView testStartView;
        private Test test;

        public TestStartViewPresenter(IParentView parentView, ITestStartView testStartView, Test test)
        {
            this.testStartView = testStartView;
            this.test = test;
            testStartView.Show(parentView);
            testStartView.ShowTestInfo(
                test.Name,
                test.QuestionCount,
                test.Duration);
        }
    }
}