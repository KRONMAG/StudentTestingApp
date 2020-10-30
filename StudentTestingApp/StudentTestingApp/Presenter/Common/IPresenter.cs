namespace StudentTestingApp.Presenter.Common
{
    /// <summary>
    /// Представитель
    /// </summary>
    public interface IPresenter
    {
        /// <summary>
        /// Запуск представителя: настройка и показ представления
        /// </summary>
        void Run();
    }

    /// <summary>
    /// Параметризованный представитель
    /// </summary>
    /// <typeparam name="T">Тип параметра представителя</typeparam>
    public interface IPresenter<T>
    {
        /// <summary>
        /// Запуск представителя с параметром: настройка и показ представления
        /// </summary>
        /// <param name="parameter">Параметр, передаваемый представителю</param>
        void Run(T parameter);
    }
}