using System;
using StudentTestingApp.Model;

namespace StudentTestingApp.View.Interface
{
    public interface ITestStartView : IDerivedView
    {
        void ShowTestInfo(Test test);
        void ShowError(string message);
        string StudentName { get; }
        event Action OnStartTest;
    }
}