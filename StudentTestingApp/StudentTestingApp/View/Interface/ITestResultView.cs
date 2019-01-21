using System;

namespace StudentTestingApp.View.Interface
{
    public interface ITestResultView : IView
    {
        void SetTestResult(int elapsedTime, double result);
        void ShowMessage(string message);
        event Action DelayedResultUploadingSelected;
    }
}