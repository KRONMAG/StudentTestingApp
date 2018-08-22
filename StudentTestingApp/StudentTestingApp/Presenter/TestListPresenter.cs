using System;
using System.Threading.Tasks;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class TestListPresenter : IPresenter
    {
        private IParentView parentView;
        private ITestListView testListView;
        private Subject subject;

        public TestListPresenter(IParentView parentView, ITestListView testListView, Subject subject)
        {
            this.parentView = parentView;
            this.testListView = testListView;
            this.subject = subject;
            testListView.OnSelectTest += selectTest;
        }

        public void Run()
        {
            testListView.Show(parentView);
            new Task(() =>
            {
                var test = DB.Instance.GetTests(subject);
                if (test != null)
                    testListView.SetTests(test);
            }).Start();
        }

        private void selectTest()
        {
            testListView.Close();
            new TestStartPresenter(
                parentView,
                App.Container.Resolve<ITestStartView>(),
                testListView.SelectedTest).Run();
        }
    }
}