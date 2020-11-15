using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;
using Xamarin.Essentials;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления предварительной настройки приложения
    /// </summary>
    public class PreloadPresenter : BasePresenter<IPreloadView, bool>
    {
        private readonly IMessageDialog _messageDialog;

        /// <summary>
        /// Загрузчик базы данных тестов
        /// </summary>
        private readonly TestsLoader _testsLoader;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представление предварительной настройки приложения</param>
        /// <param name="testsLoader">Загрузчик базы данных тестов</param>
        public PreloadPresenter
            (ApplicationController controller,
            IPreloadView view,
            IMessageDialog messageDialog,
            TestsLoader testsLoader) :
            base(controller, view)
        {
            _messageDialog = messageDialog;
            _testsLoader = testsLoader;
        }

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
                        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                            _messageDialog.ShowMessage("Невозможно обновить тесты: отсутствует интернет-соединение");
                        else if (_testsLoader.HaveTestsBeenUpdated)
                            _messageDialog.ShowMessage("Загружена последняя версия тестов, обновление не требуется");
                        else if (!_testsLoader.LoadTests())
                            _messageDialog.ShowMessage("При обновлении тестов возникла ошибка");
                        else
                            _messageDialog.ShowMessage("Тесты успешно обновлены");
                    }
                    else
                    {
                        view.ShowStepName("Загрузка тестов");
                        if (!_testsLoader.HaveTestsBeenLoaded)
                            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                                _messageDialog.ShowMessage("Невозможно загрузить тесты: отсутствует интернет-соединение");
                            else if (!_testsLoader.LoadTests())
                                _messageDialog.ShowMessage("При загрузке тестов возникла ошибка");
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