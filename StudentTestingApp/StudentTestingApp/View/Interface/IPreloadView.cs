namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Представление предварительной настройки приложения
    /// </summary>
    public interface IPreloadView : IView
    {
        /// <summary>
        /// Показ наименования текущего шага процесса предварительной настройки приложения
        /// </summary>
        /// <param name="header">Наименование шага</param>
        void ShowStepName(string header);

        /// <summary>
        /// Показ сообщения
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        void ShowMessage(string message);
    }
}