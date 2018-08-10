using System;

namespace StudentTestingApp.View.Interface
{
    interface IPreloadView : IParentView
    {
        void ShowError(string message);
    }
}
