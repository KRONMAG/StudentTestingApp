using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    public interface ISubjectsView : IView
    {
        event Action SubjectSelected;
        int SelectedSubjectId { get; }
        void SetSubjects(IReadOnlyList<Tuple<int, string>> subjects);
    }
}