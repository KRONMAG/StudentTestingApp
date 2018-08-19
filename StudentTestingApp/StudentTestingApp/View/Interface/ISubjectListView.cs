using System;
using StudentTestingApp.Model;

namespace StudentTestingApp.View.Interface
{
    public interface ISubjectListView : IView
    {
        void AddSubject(Subject subject);
        Subject SelectedSubject { get; }
        event Action OnSelectSubject;
    }
}