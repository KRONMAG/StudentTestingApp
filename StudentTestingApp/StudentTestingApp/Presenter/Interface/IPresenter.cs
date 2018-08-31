namespace StudentTestingApp.Presenter.Interface
{
    public interface IPresenter
    {
        void Run();
    }

    public interface IPresenter<in T>
    {
        void Run(T parameter);
    }
}