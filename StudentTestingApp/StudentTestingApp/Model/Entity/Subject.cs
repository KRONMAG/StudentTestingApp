using SQLite;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.Entity
{
    /// <summary>
    /// Учебный предмет
    /// </summary>
    [Table("subjects")]
    public class Subject : IEntity
    {
        /// <summary>
        /// Идентификатор учебного предмета
        /// </summary>
        [Column("id"), PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Наименование учебного предмета
        /// </summary>
        [Column("name"), NotNull]
        public string Name { get; set; }
    }
}