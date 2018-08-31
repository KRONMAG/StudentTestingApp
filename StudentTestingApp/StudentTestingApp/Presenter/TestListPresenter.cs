using System;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class TestListPresenter : IPresenter<Subject>
    {
        private readonly ITestListView _testListView;
        private readonly IReadOnlyRepository<Test> _testRepository;

        public TestListPresenter(ITestListView testListView, IReadOnlyRepository<Test> testRepository)
        {
            _testListView = testListView;
            _testRepository = testRepository;
        }

        public void Run(Subject parameter)
        {
            var tests = _testRepository.GetItems(test => test.SubjectId == parameter.Id)
                .Select(test => new Tuple<int, string>(test.Id, test.Name));
            _testListView.SetTests(tests);
            _testListView.OnSelectTest += SelectTest;
            _testListView.Show();
        }

        private void SelectTest()
        {
            _testListView.Close();
            var selectedTest = _testRepository.GetItem(_testListView.SelectedTestId);
            ApplicationController.Instance.Run<TestStartPresenter, Test>(selectedTest);
        }
    }
}