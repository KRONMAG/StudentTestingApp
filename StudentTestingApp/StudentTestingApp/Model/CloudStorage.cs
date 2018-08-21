using System;
using System.IO;
using Dropbox.Api;

namespace StudentTestingApp.Model
{
    public class CloudStorage
    {

        public bool DownloadFile(string remoteFileName, string localFileName)
        {
            try
            {
                var client = new DropboxClient("xyKd_Ut2WXAAAAAAAAAAJRLFuvca789YP8P9OaXw1JBJb4s3wji3b2a78bdeExu3");
                var buffer = client.Files.DownloadAsync(remoteFileName).Result.GetContentAsByteArrayAsync().Result;
                File.WriteAllBytes(localFileName, buffer);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}