namespace StudentTestingApp.Model.DataAccess.Interface
{
    public interface ITestsLoader
    {
        bool TestsAreLoaded { get; }
        bool TestsAreUpdated { get; }
        bool IsInternetConnectionActive { get; }
        bool LoadTests();
    }
}