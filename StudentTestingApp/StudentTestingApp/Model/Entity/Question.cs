using System;
using SQLite;

namespace StudentTestingApp.Model.Entity
{
    [Table("questions")]
    public class Question
    {
        [Column("id"), PrimaryKey]
        public int Id { get; set; }

        [Column("test_id"), NotNull]
        public int TestId { get; set; }

        [Column("text"), NotNull]
        public string Text { get; set; }

        [Column("image")]
        public byte[] Image { get; set; }
    }
}