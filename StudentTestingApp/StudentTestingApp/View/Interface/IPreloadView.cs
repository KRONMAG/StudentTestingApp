namespace StudentTestingApp.View.Interface
{
    public interface IPreloadView : IView
    {
        void SetProcessName(string header);
        void ShowMessage(string message);
    }
}