using System;
using SQLite;

namespace StudentTestingApp.Model
{
    [Table("answers")]
    public class Answer
    {
        [Column("question_id"), NotNull]
        public int QuestionId { get; set; }

        [Column("right"), NotNull]
        public bool Right { get; set; }

        [Column("text"), NotNull]
        public string Text { get; set; }
    }
}