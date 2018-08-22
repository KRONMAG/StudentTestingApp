﻿using System;
using SQLite;

namespace StudentTestingApp.Model.Entity
{
    [Table("tests")]
    public class Test
    {
        [Column("id"), PrimaryKey]
        public int Id { get; set; }

        [Column("subject_id"), NotNull]
        public int SubjectId { get; set; }

        [Column("name"), NotNull]
        public string Name { get; set; }

        [Column("question_count"), NotNull]
        public int QuestionCount { get; set; }

        [Column("duration")]
        public int? Duration { get; set; }
    }
}