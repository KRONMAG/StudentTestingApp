using StudentTestingApp.Model.Entity.Interface;

namespace StudentTestingApp.Model.Entity
{
    /// <summary>
    /// Статистика по количеству полученных оценок
    /// </summary>
    public class MarksStatistics : IEntity
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Год начала учебного года
        /// </summary>
        public int StartYear { get; set; }

        /// <summary>
        /// Год конца учебного года
        /// </summary>
        public int EndYear { get; set; }

        /// <summary>
        /// Количество полученных двоек
        /// </summary>
        public int TwosCount { get; set; }

        /// <summary>
        /// Количество полученных троек
        /// </summary>
        public int ThreesCount { get; set; }

        /// <summary>
        /// Количество полученных четверок
        /// </summary>
        public int FoursCount { get; set; }

        /// <summary>
        /// Количество полученных пятерок
        /// </summary>
        public int FivesCount { get; set; }
    }
}