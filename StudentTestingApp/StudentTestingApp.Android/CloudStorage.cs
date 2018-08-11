using System;
using System.IO;
using Xamarin.Forms;
using Dropbox.Api;
using StudentTestingApp.Model;

[assembly: Dependency(typeof(StudentTestingApp.Droid.CloudStorage))]
namespace StudentTestingApp.Droid
{
    public class CloudStorage : ICloudStorage
    {

        public bool DownloadFile(string remoteFileName, string localFileName)
        {
            try
            {
                var client = new DropboxClient("xyKd_Ut2WXAAAAAAAAAAGUfnm2lG7dz9OpPEnB0c3Z2lHXvMskst5Cp0sbWWo6Ty");
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