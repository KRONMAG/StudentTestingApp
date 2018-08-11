using System;
using SQLite;

namespace StudentTestingApp.Model
{
    [Table("subjects")]
    public class Subject
    {
        [Column("id"), PrimaryKey]
        public int Id { get; set; }

        [Column("name"), NotNull]
        public string Name { get; set; }
    }
}