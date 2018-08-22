using System;
using StudentTestingApp.Model.Entity;

namespace StudentTestingApp.View.Interface
{
    public interface ITestStartView : IDerivedView
    {
        void ShowError(string message);
        void SetTest(Test test);
        string StudentName { get; }
        event Action OnStartTest;
    }
}