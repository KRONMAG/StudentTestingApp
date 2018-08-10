using System;
using SQLite;

namespace StudentTestingApp.Model
{
    [Table("subjects")]
    class Subject
    {
        [Column("id"), PrimaryKey]
        public int Id { get; set; }

        [Column("name"), NotNull, Unique]
        public string Name { get; set; }
    }
}