namespace StudentTestingApp.Presenter.Common
{
    public interface IPresenter
    {
        void Run();
    }

    public interface IPresenter<T>
    {
        void Run(T parameter);
    }
}