using System;
using SQLite;

namespace StudentTestingApp.Model
{
    [Table("questions")]
    class Question
    {
        [Column("id"), PrimaryKey]
        public int Id { get; set; }

        [Column("test_id"), NotNull]
        public int TestId { get; set; }

        [Column("text"), NotNull]
        public string Text { get; set; }

        [Column("img_src")]
        public string ImgSrc { get; set; }
    }
}
