using System.Threading.Tasks;
using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class SettingsPresenter : IPresenter
    {
        private ISettingsView _settingsView;
        private ITestsLoader _testsLoader;

        public SettingsPresenter(ISettingsView settingsView, ITestsLoader testsLoader)
        {
            _settingsView = settingsView;
            _testsLoader = testsLoader;
        }

        public void Run()
        {
            _settingsView.TestsUpdateSelected += TestsUpdateSelected;
            _settingsView.Show();
        }

        private void TestsUpdateSelected()
        {
            Task.Run(() =>
            {
                if (!_testsLoader.InternetConnectionIsActive)
                    _settingsView.ShowMessage("Отсутствует интернет-соединение для проверки обновлений");
                else if (_testsLoader.TestsAreUpdated)
                    _settingsView.ShowMessage("Загружена последняя версия тестов, обновление не требуется");
                else if (!_testsLoader.LoadTests())
                    _settingsView.ShowMessage("При обновлении тестов возникла ошибка");
                else
                    _settingsView.ShowMessage("Тесты успешно обновлены");
            });
        }
    }
}