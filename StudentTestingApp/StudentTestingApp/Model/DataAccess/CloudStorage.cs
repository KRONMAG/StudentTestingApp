using System;
using System.IO;
using Dropbox.Api;

namespace StudentTestingApp.Model.DataAccess
{
    public class CloudStorage
    {

        public bool DownloadFile(string remoteFileName, string localFileName)
        {
            try
            {
                var client = new DropboxClient("xyKd_Ut2WXAAAAAAAAAAJxL6dlFCA8WSRjC_BYZTQpHd2tYan9fY2ezY9oTfZECc");
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