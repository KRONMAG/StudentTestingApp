using System;
using SQLite;

namespace StudentTestingApp.Model
{
    [Table("tests")]
    class Test
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("subject_id"), NotNull]
        public int SubjectId { get; set; }

        [Column("name"), NotNull, Unique]
        public string Name { get; set; }

        [Column("question_count"), NotNull]
        public int QuestionCount { get; set; }

        [Column("duration")]
        public int? Duration { get; set; }
    }
}
