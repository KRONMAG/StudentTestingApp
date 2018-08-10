using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    class TestListViewPresenter
    {
        private IParentView parentView;
        private ITestListView testListView;
        private int subjectId;
        private IEnumerable<Test> tests;

        public TestListViewPresenter(IParentView parentView, ITestListView testListView, int subjectId)
        {
            this.testListView = testListView;
            this.parentView = parentView;
            this.subjectId = subjectId;
            testListView.Show(parentView);
            testListView.OnSelectTest += selectTest;
            new Task(() =>
            {
                tests = DB.Instance.GetTests(subjectId);
                foreach (var item in tests)
                    testListView.ShowTest(item.Name);
            }).Start();
        }

        private void selectTest()
        {
            foreach (var item in tests)
                if (item.Name == testListView.SelectedTestName)
                {
                    new TestStartViewPresenter(
                        parentView,
                        App.Container.Resolve<ITestStartView>(),
                        item);
                    break;
                }
        }
    }
}