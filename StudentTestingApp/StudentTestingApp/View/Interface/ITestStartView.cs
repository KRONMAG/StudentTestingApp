using System;

namespace StudentTestingApp.View.Interface
{
    public interface ITestStartView : IView
    {
        string StudentName { get; }
        void ShowError(string message);
        void SetTest(string name, int questionCount, int? duration);
        event Action OnStartTest;
    }
}