using SQLite;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.Entity
{
    [Table("questions")]
    public class Question : IEntity
    {
        [Column("id"), PrimaryKey] public int Id { get; set; }

        [Column("test_id"), NotNull] public int TestId { get; set; }

        [Column("text"), NotNull] public string Text { get; set; }

        [Column("image")] public byte[] Image { get; set; }
    }
}