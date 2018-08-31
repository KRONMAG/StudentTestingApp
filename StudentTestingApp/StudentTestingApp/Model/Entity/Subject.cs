using SQLite;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.Entity
{
    [Table("subjects")]
    public class Subject : IEntity
    {
        [Column("id"), PrimaryKey] public int Id { get; set; }

        [Column("name"), NotNull] public string Name { get; set; }
    }
}