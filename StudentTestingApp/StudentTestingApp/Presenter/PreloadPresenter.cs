using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;
using Xamarin.Essentials;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления предварительной настройки приложения
    /// </summary>
    public class PreloadPresenter : BasePresenter<IPreloadView>
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
        /// Загрузка базы данных тестов, показ представления
        /// </summary>
        public override void Run()
        {
            Worker.Run
            (
                () =>
                {
                    if (!_testsLoader.HaveTestsBeenLoaded)
                        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                            _messageDialog.ShowMessage("Невозможно загрузить тесты: отсутствует интернет-соединение");
                        else if (!_testsLoader.LoadTests())
                            _messageDialog.ShowMessage("При загрузке тестов возникла ошибка");
                    return true;
                },
                _ => controller.CreatePresenter<MainPresenter>().Run(),
                _ => { }
            );
            base.Run();
        }
    }
}