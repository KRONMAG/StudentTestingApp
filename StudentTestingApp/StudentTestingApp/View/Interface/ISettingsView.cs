using System;

namespace StudentTestingApp.View.Interface
{
    public interface ISettingsView : IView
    {
        event Action UpdateTestsSelected;
        void ShowMessage(string message);
    }
}