using SQLite;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.Entity
{
    [Table("answers")]
    public class Answer : IEntity
    {
        [Column("id"), PrimaryKey]
        public int Id { get; set; }

        [Column("question_id"), NotNull]
        public int QuestionId { get; set; }

        [Column("right"), NotNull]
        public bool Right { get; set; }

        [Column("text"), NotNull]
        public string Text { get; set; }
    }
}