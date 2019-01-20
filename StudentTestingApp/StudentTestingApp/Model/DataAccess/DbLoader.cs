using System.IO;
using SQLite;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;

namespace StudentTestingApp.Model.DataAccess
{
    public class DbLoader : IDbLoader
    {
        #region IDbLoader

        public bool DbExist => File.Exists(DbInfo.Instance.DbLocalFilePath);

        public void CreateEmptyDb()
        {
            var db = new SQLiteConnection(DbInfo.Instance.DbLocalFilePath);
            db.CreateTable<Subject>();
            db.CreateTable<Test>();
            db.CreateTable<Question>();
            db.CreateTable<Answer>();
            db.CreateTable<TestResult>();
        }

        public bool LoadDb()
        {
            var cloudStorage = new CloudStorage();
            return cloudStorage.DownloadFile($"/{DbInfo.Instance.DbRemoteFilePath}", DbInfo.Instance.DbLocalFilePath);
        }
        
        #endregion
    }
}