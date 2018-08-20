using System;
using System.Collections.Generic;
using StudentTestingApp.Model;

namespace StudentTestingApp.View.Interface
{
    public interface ISubjectListView : IView
    {
        void SetSubjects(IEnumerable<Subject> subjects);
        Subject SelectedSubject { get; }
        event Action OnSelectSubject;
    }
}