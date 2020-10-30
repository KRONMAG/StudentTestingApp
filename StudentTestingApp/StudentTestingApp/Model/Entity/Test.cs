using SQLite;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.Entity
{
    /// <summary>
    /// Тест
    /// </summary>
    [Table("tests")]
    public class Test : IEntity
    {
        /// <summary>
        /// Идентификатор теста
        /// </summary>
        [Column("id"), PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор учебного предмета, которому принадлежит тест
        /// </summary>
        [Column("subject_id"), NotNull]
        public int SubjectId { get; set; }

        /// <summary>
        /// Название теста
        /// </summary>
        [Column("name"), NotNull]
        public string Name { get; set; }

        /// <summary>
        /// Количество вопросов, отбираемое из пула вопросов
        /// теста для проведения тестирования
        /// </summary>
        [Column("questions_count"), NotNull]
        public int QuestionsCount { get; set; }

        /// <summary>
        /// Продолжительность тестирования в секундах
        /// Если свойство имеет значение null, продолжительность не ограничена
        /// </summary>
        [Column("duration")]
        public int? Duration { get; set; }
    }
}