using System;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class TestStartPresenter : BasePresenter<ITestStartView, Test>
    {
        private Test _test;

        public TestStartPresenter(ApplicationController controller, ITestStartView view) :
            base(controller, view) =>
            view.TestStarted += TestStarted;

        private void TestStarted() =>
            controller.CreatePresenter<TestNavigationPresenter, Test>().Run(_test);

        public override void Run(Test test)
        {
            _test = test;
            view.SetTest(test.Name, test.QuestionsCount, test.Duration);
            view.Show();
        }
    }
}