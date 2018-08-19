using System;
using StudentTestingApp.Model;

namespace StudentTestingApp.View.Interface
{
    public interface ITestStartView : IView
    {
        void ShowError(string message);
        void SetTest(Test test);
        string StudentName { get; }
        event Action OnStartTest;
    }
}