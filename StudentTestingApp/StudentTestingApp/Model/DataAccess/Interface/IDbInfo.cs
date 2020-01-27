namespace StudentTestingApp.Model.DataAccess.Interface
{
    public interface IDbInfo
    {
        string TestsDbFileName { get; }
        string TestsDbLocalFilePath { get; }
        string TestsDbRemoteFilePath { get; }
        string AppDbFileName { get; }
        string AppDbFilePath { get; }
    }
}