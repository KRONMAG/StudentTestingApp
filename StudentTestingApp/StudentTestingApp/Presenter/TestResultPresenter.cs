using StudentTestingApp.Model.Entity;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Common;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления результата тестирования
    /// </summary>
    public class TestResultPresenter : BasePresenter<ITestResultView, TestResult>
    {
        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представление результата тестирования</param>
        public TestResultPresenter(ApplicationController controller, ITestResultView view) :
            base(controller, view) =>
            view.GoToMainView += GoToMainView;

        /// <summary>
        /// Обработчик запроса перехода к представлению меню приложения
        /// </summary>
        private void GoToMainView() =>
            controller.CreatePresenter<MainPresenter>().Run();

        /// <summary>
        /// Показ результата тестирования, представления
        /// </summary>
        /// <param name="testResult">Результат тестирования</param>
        public override void Run(TestResult testResult)
        {
            view.ShowTestResult
            (
                (int)(testResult.EndDate - testResult.StartDate).TotalSeconds,
                testResult.Score
            );
            base.Run(testResult);
        }
    }
}