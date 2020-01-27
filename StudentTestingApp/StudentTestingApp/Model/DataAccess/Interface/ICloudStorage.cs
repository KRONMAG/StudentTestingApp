namespace StudentTestingApp.Model.DataAccess.Interface
{
    public interface ICloudStorage
    {
        bool DownloadFile(string remoteFilePath, string localFilePath);
        string GetFileHash(string filePath);
    }
}
