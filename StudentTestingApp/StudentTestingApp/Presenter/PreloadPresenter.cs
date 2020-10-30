using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления предварительной настройки приложения
    /// </summary>
    public class PreloadPresenter : BasePresenter<IPreloadView, bool>
    {
        /// <summary>
        /// Загрузчик базы данных тестов
        /// </summary>
        private readonly ITestsLoader _testsLoader;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представление предварительной настройки приложения</param>
        /// <param name="testsLoader">Загрузчик базы данных тестов</param>
        public PreloadPresenter
            (ApplicationController controller,
            IPreloadView view,
            ITestsLoader testsLoader) :
            base(controller, view) =>
            _testsLoader = testsLoader;

        /// <summary>
        /// Загрузка (обновление) базы данных тестов, показ представления
        /// </summary>
        /// <param name="updateTests">
        /// Требуется ли выполнить обновление базы данных тестов вместо ее загрузки
        /// </param>
        public override void Run(bool updateTests = false)
        {
            Worker.Run
            (
                () =>
                {
                    if (updateTests && _testsLoader.HaveTestsBeenLoaded)
                    {
                        view.ShowStepName("Обновление тестов");
                        if (!_testsLoader.IsInternetConnectionActive)
                            view.ShowMessage("Невозможно обновить тесты: отсутствует интернет-соединение");
                        else if (_testsLoader.HaveTestsBeenUpdated)
                            view.ShowMessage("Загружена последняя версия тестов, обновление не требуется");
                        else if (!_testsLoader.LoadTests())
                            view.ShowMessage("При обновлении тестов возникла ошибка");
                        else
                            view.ShowMessage("Тесты успешно обновлены");
                    }
                    else
                    {
                        view.ShowStepName("Загрузка тестов");
                        if (!_testsLoader.HaveTestsBeenLoaded)
                            if (!_testsLoader.IsInternetConnectionActive)
                                view.ShowMessage("Невозможно загрузить тесты: отсутствует интернет-соединение");
                            else if (!_testsLoader.LoadTests())
                                view.ShowMessage("При загрузке тестов возникла ошибка");
                    }
                    return true;
                },
                _ => controller.CreatePresenter<MainPresenter>().Run(),
                _ => { }
            );
            base.Run(updateTests);
        }
    }
}