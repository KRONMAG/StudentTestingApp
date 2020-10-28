using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.Model.DataAccess.Interface;

namespace StudentTestingApp.Presenter
{
    public class SettingsPresenter : BasePresenter<ISettingsView>
    {
        private readonly ITestsLoader _testsLoader;

        public SettingsPresenter(ApplicationController controller, ISettingsView view, ITestsLoader testsLoader) :
            base(controller, view)
        {
            _testsLoader = testsLoader;
            view.UpdateTestsSelected += UpdateTestSelected;
        }

        private void UpdateTestSelected()
        {
            if (!_testsLoader.IsInternetConnectionActive)
                view.ShowMessage("Отсутствует интернет-соединение для проверки обновлений");
            else if (_testsLoader.TestsAreUpdated)
                view.ShowMessage("Загружена последняя версия тестов, обновление не требуется");
            else
                Worker.Run
                (
                    () => _testsLoader.LoadTests(),
                    result =>
                    {
                        if (result)
                        {
                            view.ShowMessage("Тесты успешно обновлены");
                            controller.CreatePresenter<MainPresenter>().Run();
                        }
                        else
                            view.ShowMessage("При обновлении тестов возникла ошибка");
                    },
                    _ => { }
                );
        }
    }
}