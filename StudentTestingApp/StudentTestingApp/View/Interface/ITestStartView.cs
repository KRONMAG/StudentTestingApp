using System;

namespace StudentTestingApp.View.Interface
{
    public interface ITestStartView : IView
    {
        string StudentName { get; }
        void ShowMessage(string message);
        void SetTest(string name, int questionCount, int? duration);
        event Action TestStarted;
    }
}