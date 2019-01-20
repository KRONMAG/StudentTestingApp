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
                DropboxClient client =
                    new DropboxClient("xyKd_Ut2WXAAAAAAAAAAKQlELmi36N5klGN6BCfXBGCKC8vyFk4N57hPPofyyWvb");
                byte[] buffer = client.Files.DownloadAsync(remoteFileName).Result.GetContentAsByteArrayAsync().Result;
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