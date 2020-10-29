using System;
using SQLite;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.Entity
{
    [Table("test_results")]
    public class TestResult : IEntity
    {
        [Column("id"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("subject_name"), NotNull]
        public string SubjectName { get; set; }

        [Column("test_name"), NotNull]
        public string TestName { get; set; }

        [Column("start_date"), NotNull]
        public DateTime StartDate { get; set; }
        
        [Column("end_date"), NotNull]
        public DateTime EndDate { get; set; }

        [Column("score"), NotNull]
        public double Score { get; set; }
    }
}