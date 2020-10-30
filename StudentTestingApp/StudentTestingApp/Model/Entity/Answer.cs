using SQLite;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.Entity
{
    /// <summary>
    /// Вариант ответа на вопрос теста
    /// </summary>
    [Table("answers")]
    public class Answer : IEntity
    {
        /// <summary>
        /// Идентификатор варианта ответа
        /// </summary>
        [Column("id"), PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор вопроса, к которому относится вариант ответа
        /// </summary>
        [Column("question_id"), NotNull]
        public int QuestionId { get; set; }

        /// <summary>
        /// Является ли вариант ответа правильным
        /// </summary>
        [Column("right"), NotNull]
        public bool Right { get; set; }

        /// <summary>
        /// Текст варианта ответа
        /// </summary>
        [Column("text"), NotNull]
        public string Text { get; set; }
    }
}