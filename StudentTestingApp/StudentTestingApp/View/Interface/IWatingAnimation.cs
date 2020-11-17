using System;

namespace StudentTestingApp.View.Interface
{
    /// <summary>
    /// Функционал показа анимации ожидания
    /// </summary>
    public interface IWaitingAnimation
    {
        /// <summary>
        /// Запуск анимации ожидания с заданным сообщением
        /// </summary>
        /// <param name="message">Сообщение, отображаемое рядом с анимацией ожидания</param>
        /// <param name="guid">Идентификатор анимации</param>
        void StartAnimation(string message, out Guid guid);

        /// <summary>
        /// Остановка анимации ожидания с указанным идентификатором
        /// </summary>
        /// <param name="guid">Идентификатор анимации</param>
        void StopAnimation(Guid guid);
    }
}
