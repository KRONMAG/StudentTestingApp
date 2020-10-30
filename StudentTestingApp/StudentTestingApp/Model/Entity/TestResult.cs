using System;
using SQLite;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.Entity
{
    /// <summary>
    /// Результат тестирования
    /// </summary>
    [Table("test_results")]
    public class TestResult : IEntity
    {
        /// <summary>
        /// Идентификатор результата тестирования
        /// </summary>
        [Column("id"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Название учебного предмета пройденного теста
        /// </summary>
        [Column("subject_name"), NotNull]
        public string SubjectName { get; set; }

        /// <summary>
        /// Наименование пройденного теста
        /// </summary>
        [Column("test_name"), NotNull]
        public string TestName { get; set; }

        /// <summary>
        /// Дата и время начала тестирования
        /// </summary>
        [Column("start_date"), NotNull]
        public DateTime StartDate { get; set; }
        
        /// <summary>
        /// Дата и время окончания тестирования
        /// </summary>
        [Column("end_date"), NotNull]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Процент правильных ответов
        /// </summary>
        [Column("score"), NotNull]
        public decimal Score { get; set; }
    }
}