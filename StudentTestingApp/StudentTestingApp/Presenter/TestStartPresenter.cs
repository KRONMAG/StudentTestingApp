using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления отображения данных выбранного теста
    /// </summary>
    public class TestStartPresenter : BasePresenter<ITestStartView, Test>
    {
        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представление отображения данных выбранного теста</param>
        public TestStartPresenter(ApplicationController controller, ITestStartView view) :
            base(controller, view) =>
            view.StartTest += StartTest;

        /// <summary>
        /// Обработчик запроса начала тестирования
        /// </summary>
        private void StartTest() =>
            controller.CreatePresenter<TestNavigationPresenter, Test>().Run(parameter);

        /// <summary>
        /// Показ данных о выбранном тесте, представления
        /// </summary>
        /// <param name="test"></param>
        public override void Run(Test test)
        {
            view.ShowTestInfo(test.Name, test.QuestionsCount, test.Duration);
            base.Run(test);
        }
    }
}