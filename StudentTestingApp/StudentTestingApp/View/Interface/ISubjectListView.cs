using System;

namespace StudentTestingApp.View.Interface
{
    interface ISubjectListView : IParentView
    {
        void ShowSubject(string name);
        string SelectedSubjectName { get; }
        event Action OnSelectSubject;
    }
}