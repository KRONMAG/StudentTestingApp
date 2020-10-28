using System.IO;
using Dropbox.Api;

namespace StudentTestingApp.Model.DataAccess
{
    public class CloudStorage
    {
        private const string ACCESS_TOKEN = "QiNJ84AteaUAAAAAAAAAAWNpABuTzBfouVCnhUDdfzBtcmq5TvADh39UmsoRmssR";

        public bool DownloadFile(string remoteFilePath, string localFilePath)
        {
            try
            {
                var client = new DropboxClient(ACCESS_TOKEN);
                var buffer = client.Files
                    .DownloadAsync(remoteFilePath)
                    .Result
                    .GetContentAsByteArrayAsync()
                    .Result;
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