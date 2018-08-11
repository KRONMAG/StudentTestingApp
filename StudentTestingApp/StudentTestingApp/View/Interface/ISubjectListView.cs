using System;
using System.Collections.Generic;
using StudentTestingApp.Model;

namespace StudentTestingApp.View.Interface
{
    public interface ISubjectListView : IParentView
    {
        ICollection<Subject> Subjects { get; }
        Subject SelectedSubject { get; }
        event Action OnSelectSubject;
    }
}