using System;
using System.Collections.Generic;
using StudentTestingApp.Model.Entity;

namespace StudentTestingApp.View.Interface
{
    public interface ISubjectListView : IParentView
    {
        void SetSubjects(IEnumerable<Subject> subjects);
        Subject SelectedSubject { get; }
        event Action OnSelectSubject;
    }
}