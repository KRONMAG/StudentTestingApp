using System;
using System.Threading.Tasks;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class TestListPresenter : IPresenter
    {
        private ITestListView testListView;
        private Subject subject;

        public TestListPresenter(ITestListView testListView, Subject subject)
        {
            this.testListView = testListView;
            this.subject = subject;
            testListView.OnSelectTest += selectTest;
        }

        public void Run()
        {
            testListView.Show();
            new Task(() =>
            {
                var test = DB.Instance.GetTests(subject);
                if (test != null)
                    testListView.SetTests(test);
            }).Start();
        }

        private void selectTest()
        {
            new TestStartPresenter(
                App.Container.Resolve<ITestStartView>(),
                testListView.SelectedTest).Run();
        }
    }
}