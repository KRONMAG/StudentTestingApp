namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Функционал диалоговых сообщений
    /// </summary>
    public interface IMessageDialog
    {
        /// <summary>
        /// Показ диалогового окна с указанным сообщением
        /// </summary>
        /// <param name="message">Сообщение для показа</param>
        void ShowMessage(string message);
    }
}