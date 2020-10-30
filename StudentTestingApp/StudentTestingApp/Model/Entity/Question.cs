using SQLite;
using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.Entity
{
    /// <summary>
    /// Вопрос теста
    /// </summary>
    [Table("questions")]
    public class Question : IEntity
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        [Column("id"), PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор теста, которому принадлежит вопрос
        /// </summary>
        [Column("test_id"), NotNull]
        public int TestId { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        [Column("text"), NotNull]
        public string Text { get; set; }

        /// <summary>
        /// Изображение, поясняющее текст вопроса
        /// Если свойство имеет значение null, изображение отсутствует
        /// </summary>
        [Column("image")]
        public byte[] Image { get; set; }
    }
}