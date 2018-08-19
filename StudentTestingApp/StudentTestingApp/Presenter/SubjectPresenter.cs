﻿using System;
using System.Threading.Tasks;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class SubjectPresenter
    {
        private ISubjectListView subjectListView;

        public SubjectPresenter(ISubjectListView subjectListView)
        {
            this.subjectListView = subjectListView;
            subjectListView.OnSelectSubject += selectSubject;
            subjectListView.Show();
            new Task(() =>
            {
                var subjects = DB.Instance.GetSubjects();
                foreach (var subject in subjects)
                    subjectListView.AddSubject(subject);
            }).Start();
        }

        private void selectSubject()
        {
            new TestListPresenter(
                App.Container.Resolve<ITestListView>(),
                subjectListView.SelectedSubject);
        }
    }
}