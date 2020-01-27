using System.Threading.Tasks;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class PreloadPresenter : IPresenter
    {
        private readonly IPreloadView _preloadView;
        private readonly ITestsLoader _testsLoader;

        public PreloadPresenter(IPreloadView preloadView, ITestsLoader testsLoader)
        {
            _preloadView = preloadView;
            _testsLoader = testsLoader;
        }

        public void Run()
        {
            _preloadView.Show();
            Task.Run(() =>
            {
                if (!_testsLoader.TestsAreLoaded)
                    if (!_testsLoader.InternetConnectionIsActive)
                        _preloadView.ShowMessage("Невозможно загрузить тесты: отсутствует интернет-соединения");
                    else if (!_testsLoader.LoadTests())
                        _preloadView.ShowMessage("При загрузке тестов возникла ошибка");
                _preloadView.Close();
                ApplicationController.Instance.CreatePresenter<MainPresenter>().Run();
            });
        }
    }
}