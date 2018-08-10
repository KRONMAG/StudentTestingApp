using System;
using System.Threading.Tasks;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;
using System.Collections.Generic;
using Unity;

namespace StudentTestingApp.Presenter
{
    class SubjectViewPresenter
    {
        private ISubjectListView subjectListView;
        private IEnumerable<Subject> subjects;

        public SubjectViewPresenter(ISubjectListView subjectListView)
        {
            this.subjectListView = subjectListView;
            subjectListView.Show();
            subjectListView.OnSelectSubject += selectSubject;
            new Task(() =>
            {
                subjects = DB.Instance.GetSubjects();
                foreach (var item in subjects)
                    subjectListView.ShowSubject(item.Name);
            }).Start();
        }

        private void selectSubject()
        {
            foreach (var item in subjects)
                if (item.Name == subjectListView.SelectedSubjectName)
                {
                    new TestListViewPresenter(
                        subjectListView,
                        App.Container.Resolve<ITestListView>(),
                        item.Id);
                    break;
                }
        }
    }
}