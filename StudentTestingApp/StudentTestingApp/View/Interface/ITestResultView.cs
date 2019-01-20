namespace StudentTestingApp.View.Interface
{
    public interface ITestResultView : IView
    {
        void SetTestResult(int elapsedTime, double result);
    }
}