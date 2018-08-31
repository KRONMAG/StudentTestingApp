using System;
using SQLite;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.Entity
{
    [Table("test_results")]
    public class TestResult : IEntity
    {
        [Column("id"), PrimaryKey] public int Id { get; set; }

        [Column("test_id"), NotNull] public int TestId { get; set; }

        [Column("student_name"), NotNull] public string StudentName { get; set; }

        [Column("end_date"), NotNull] public DateTime EndDate { get; set; }

        [Column("result"), NotNull] public double Result { get; set; }
    }
}