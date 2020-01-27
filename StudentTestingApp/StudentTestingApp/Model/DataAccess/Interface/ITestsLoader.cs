namespace StudentTestingApp.Model.DataAccess.Interface
{
    public interface ITestsLoader
    {
        bool TestsAreLoaded { get; }
        bool TestsAreUpdated { get; }
        bool InternetConnectionIsActive { get; }
        bool LoadTests();
    }
}