using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
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

        private string getFullPath(string path)
        {
            return $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/path";
        }

        public bool InitializeDB()
        {
            var dbFileName = "StudentTestingDb.db";
            var dbFilePath = getFullPath(dbFileName);
            var storage = new CloudStorage();
            if (File.Exists(dbFilePath) || storage.DownloadFile($"/{dbFileName}", dbFilePath))
            {
                db = new SQLiteConnection(dbFilePath);
                db.CreateTable<Subject>();
                db.CreateTable<Test>();
                db.CreateTable<Question>();
                db.CreateTable<Answer>();
                return true;
            }
            else return false;
        }

        public IEnumerable<Subject> GetSubjects()
        {
            if (db == null) return null;
            else return db.Table<Subject>();
        }

        public IEnumerable<Test> GetTests(Subject subject)
        {
            if (db == null) return null;
            else return db.Table<Test>().Where(x => x.SubjectId == subject.Id);
        }

        public IEnumerable<Question> GetQuestions(Test test)
        {
            if (db == null) return null;
            else
            {
                var random = new Random();
                return db.Table<Question>().Where(x => x.TestId == test.Id).ToList().OrderBy(x => random.Next()).Take(test.QuestionCount);
            }
        }

        public IEnumerable<Answer> GetAnswers(Question question)
        {
            if (db == null) return null;
            else
            {
                var random = new Random();
                return db.Table<Answer>().Where(x => x.QuestionId == question.Id).ToList().OrderBy(x => random.Next());
            }
        }
    }
}