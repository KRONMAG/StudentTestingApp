using System;

namespace StudentTestingApp.View.Interface
{
    public interface ITestStartView : IView
    {
        event Action StartTestSelected;
        void ShowMessage(string message);
        void SetTest(string name, int questionCount, int? duration);
    }
}