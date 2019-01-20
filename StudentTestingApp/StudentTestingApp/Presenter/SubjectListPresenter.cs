using System;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class SubjectListPresenter : IPresenter
    {
        private readonly ISubjectListView _subjectListView;
        private readonly IReadOnlyRepository<Subject> _subjectRepository;

        public SubjectListPresenter(ISubjectListView subjectListView, IReadOnlyRepository<Subject> subjectRepository)
        {
            _subjectListView = subjectListView;
            _subjectRepository = subjectRepository;
        }

        public void Run()
        {
            var subjects = _subjectRepository.GetItems()
                .Select(subject => new Tuple<int, string>(subject.Id, subject.Name));
            _subjectListView.SetSubjects(subjects);
            _subjectListView.SubjectSelected += SubjectSelected;
            _subjectListView.Show();
        }

        private void SubjectSelected()
        {
            var selectedSubject = _subjectRepository.GetItem(_subjectListView.SelectedSubjectId);
            _subjectListView.Close();
            ApplicationController.Instance.CreatePresenter<TestListPresenter, Subject>().Run(selectedSubject);
        }
    }
}