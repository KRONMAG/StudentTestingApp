using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    public interface ISubjectsView : IView
    {
        int SelectedSubjectId { get; }
        void SetSubjects(IEnumerable<Tuple<int, string>> subjects);
        event Action SubjectSelected;
    }
}