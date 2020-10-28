using System;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class SubjectsPresenter : BasePresenter<ISubjectsView>
    {
        private readonly IReadOnlyRepository<Subject> _repository;

        public SubjectsPresenter
            (ApplicationController controller,
            ISubjectsView view,
            IReadOnlyRepository<Subject> repository) :
            base(controller, view)
        {
            _repository = repository;
            view.SubjectSelected += SubjectSelected;
        }

        private void SubjectSelected() =>
            controller.CreatePresenter<TestsPresenter, Subject>().Run
            (
                _repository
                    .Get()
                    .First(subject => subject.Id == view.SelectedSubjectId)
            );

        public override void Run()
        {
            view.SetSubjects
            (
                _repository
                    .Get()
                    .Select(subject => new Tuple<int, string>(subject.Id, subject.Name))
                    .ToList()
            );
            view.Show();
        }
    }
}