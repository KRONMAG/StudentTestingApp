using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class PreloadPresenter : BasePresenter<IPreloadView, bool>
    {
        private readonly ITestsLoader _testsLoader;

        public PreloadPresenter
            (ApplicationController controller,
            IPreloadView view, ITestsLoader testsLoader) :
            base(controller, view) =>
            _testsLoader = testsLoader;

        public override void Run(bool updateTests)
        {
            base.parameter = updateTests;
            Worker.Run
            (
                () =>
                {
                    if (updateTests)
                    {
                        view.SetProcessName("Обновление тестов");
                        if (!_testsLoader.IsInternetConnectionActive)
                            view.ShowMessage("Невозможно обновить тесты: отсутствует интернет-соединение");
                        else if (_testsLoader.TestsAreUpdated)
                            view.ShowMessage("Загружена последняя версия тестов, обновление не требуется");
                        else if (!_testsLoader.LoadTests())
                            view.ShowMessage("При обновлении тестов возникла ошибка");
                        else
                            view.ShowMessage("Тесты успешно обновлены");
                    }
                    else
                    {
                        view.SetProcessName("Загрузка тестов");
                        if (!_testsLoader.TestsAreLoaded)
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
            view.Show();
        }
    }
}