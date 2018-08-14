using System;
using System.Threading.Tasks;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    class TestListPresenter
    {
        private IParentView parentView;
        private ITestListView testListView;

        public TestListPresenter(IParentView parentView, ITestListView testListView, Subject subject)
        {
            this.testListView = testListView;
            this.parentView = parentView;
            testListView.OnSelectTest += selectTest;
            testListView.Show(parentView);
            new Task(() =>
            {
                var tests = DB.Instance.GetTests(subject);
                foreach (var test in tests)
                    testListView.Tests.Add(test);
            }).Start();
        }

        private void selectTest()
        {
            new TestStartPresenter(
                parentView,
                App.Container.Resolve<ITestStartView>(),
                testListView.SelectedTest);
        }
    }
}