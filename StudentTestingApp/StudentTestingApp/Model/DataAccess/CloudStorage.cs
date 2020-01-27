using System.IO;
using Dropbox.Api;
using StudentTestingApp.Model.DataAccess.Interface;

namespace StudentTestingApp.Model.DataAccess
{
    public class CloudStorage : ICloudStorage
    {
        private const string ACCESS_TOKEN = "ACCESS TOKEN ON DROPBOX API";

        public bool DownloadFile(string remoteFilePath, string localFilePath)
        {
            try
            {
                var client = new DropboxClient(ACCESS_TOKEN);
                var buffer = client.Files.DownloadAsync(remoteFilePath).Result.GetContentAsByteArrayAsync().Result;
                File.WriteAllBytes(localFilePath, buffer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetFileHash(string filePath)
        {
            try
            {
                var client = new DropboxClient(ACCESS_TOKEN);
                return client.Files.GetMetadataAsync(filePath).Result.AsFile.ContentHash;
            }
            catch
            {
                return null;
            }
        }
    }
}