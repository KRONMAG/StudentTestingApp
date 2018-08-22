using System;
using System.Threading.Tasks;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class SubjectPresenter : IPresenter
    {
        private ISubjectListView subjectListView;

        public SubjectPresenter(ISubjectListView subjectListView)
        {
            this.subjectListView = subjectListView;
            subjectListView.OnSelectSubject += selectSubject;
        }

        public void Run()
        {
            subjectListView.Show();
            new Task(() =>
            {
                var subjects = DB.Instance.GetSubjects();
                if (subjects != null)
                    subjectListView.SetSubjects(subjects);
            }).Start();
        }

        private void selectSubject()
        {
            new TestListPresenter(
                subjectListView,
                App.Container.Resolve<ITestListView>(),
                subjectListView.SelectedSubject).Run();
        }
    }
}