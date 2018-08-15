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
                var client = new DropboxClient("xyKd_Ut2WXAAAAAAAAAAIG_Wxd59BrNEIL2zktkoaQcJPyaycWR9j-BeMkUy6BgY");
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