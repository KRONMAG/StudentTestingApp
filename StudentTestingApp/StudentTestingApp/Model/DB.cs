using System;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;
using SQLite;

namespace StudentTestingApp.Model
{
    public class DB
    {
        private static DB instance;

        private DB()
        {

        }

        public static DB Instance
        {
            get
            {
                if (instance == null) instance = new DB();
                return instance;
            }
        }

        private SQLiteConnection db;

        public bool InitializeDB()
        {
            var dbFileName = "StudentTestingDb.db";
            var dbFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/{dbFileName}";
            var storage = DependencyService.Get<ICloudStorage>();
            if (File.Exists(dbFilePath) || storage.DownloadFile($"/{dbFileName}", dbFilePath))
            {
                db = new SQLiteConnection(dbFilePath);
                db.CreateTable<Subject>();
                db.CreateTable<Test>();
                db.CreateTable<Question>();
                return true;
            }
            else return false;
        }

        public IEnumerable<Subject> GetSubjects()
        {
            if (db == null) return null;
            else return from item in db.Table<Subject>() select item;
        }

        public IEnumerable<Test> GetTests(Subject subject)
        {
            if (db == null) return null;
            else return from item in db.Table<Test>() where item.SubjectId == subject.Id select item;
        }
    }
}