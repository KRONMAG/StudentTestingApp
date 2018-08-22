using System;

namespace StudentTestingApp.View.Interface
{
    public interface IPreloadView : IParentView
    {
        void ShowError(string message);
    }
}