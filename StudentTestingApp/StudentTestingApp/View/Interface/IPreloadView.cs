using System;

namespace StudentTestingApp.View.Interface
{
    public interface IPreloadView : IView
    {
        void ShowError(string message);
    }
}
