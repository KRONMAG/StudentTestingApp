using System;

namespace StudentTestingApp.View.Interface
{
    public interface ITestResultView : IView
    {
        event Action GoToMainViewSelected;
        void SetTestResult(int elapsedTime, double score);
        void ShowMessage(string message);
    }
}