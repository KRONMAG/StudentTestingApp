using System;
using System.Threading.Tasks;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class TestListPresenter
    {
        private ITestListView testListView;

        public TestListPresenter(ITestListView testListView, Subject subject)
        {
            this.testListView = testListView;
            testListView.OnSelectTest += selectTest;
            testListView.Show();
            new Task(() =>
            {
                var tests = DB.Instance.GetTests(subject);
                foreach (var test in tests)
                    testListView.AddTest(test);
            }).Start();
        }

        private void selectTest()
        {
            new TestStartPresenter(
                App.Container.Resolve<ITestStartView>(),
                testListView.SelectedTest);
        }
    }
}