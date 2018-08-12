using System;
using StudentTestingApp.Model;

namespace StudentTestingApp.View.Interface
{
    public interface ITestStartView : IDerivedView
    {
        void ShowError(string message);
        Test Test { get; set; }
        string StudentName { get; }
        event Action OnStartTest;
    }
}