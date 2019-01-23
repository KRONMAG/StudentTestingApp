using System;

namespace StudentTestingApp.View.Interface
{
    interface IMainView : IView
    {
        event Action SubjectListViewSelected;
    }
}