using System;

namespace StudentTestingApp.Model
{
    public interface ICloudStorage
    {
        bool DownloadFile(string remoteFileName, string localFileName);
    }
}
