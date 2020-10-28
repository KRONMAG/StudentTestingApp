using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class PreloadPresenter : BasePresenter<IPreloadView>
    {
        private readonly ITestsLoader _testsLoader;

        public PreloadPresenter
            (ApplicationController controller,
            IPreloadView view, ITestsLoader testsLoader) :
            base(controller, view) =>
            _testsLoader = testsLoader;

        public override void Run()
        {
            view.Show();
            Worker.Run
            (
                () =>
                {
                    if (!_testsLoader.TestsAreLoaded)
                        if (!_testsLoader.IsInternetConnectionActive)
                            view.ShowMessage("Невозможно загрузить тесты: отсутствует интернет-соединения");
                        else if (!_testsLoader.LoadTests())
                            view.ShowMessage("При загрузке тестов возникла ошибка");
                    return true;
                },
                _ => controller.CreatePresenter<MainPresenter>().Run(),
                _ => { }
            );
        }
    }
}