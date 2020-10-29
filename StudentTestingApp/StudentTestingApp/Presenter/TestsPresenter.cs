using System;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class TestsPresenter : BasePresenter<ITestsView, Subject>
    {
        private readonly IReadOnlyRepository<Test> _repository;

        public TestsPresenter(ApplicationController controller, ITestsView view, IReadOnlyRepository<Test> repository) :
            base(controller, view)
        {
            _repository = repository;
            view.TestSelected += TestSelected;
        }

        private void TestSelected() =>
            controller.CreatePresenter<TestStartPresenter, Test>().Run
            (
                _repository.Get(view.SelectedTestId)
            );

        public override void Run(Subject subject)
        {
            view.SetTests
            (
                _repository
                    .Get(test => test.SubjectId == subject.Id)
                    .Select(test => new Tuple<int, string>(test.Id, test.Name))
                    .ToList()
            );
            view.Show();
        }
    }
}