using System;

namespace StudentTestingApp.View.Interface
{
    public interface ISettingsView : IView
    {
        event Action TestsUpdateSelected;
        void ShowMessage(string message);
    }
}