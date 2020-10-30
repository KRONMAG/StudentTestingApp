using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления главного меню приложения
    /// </summary>
    public class MainPresenter : BasePresenter<IMainView>
    {
        /// <summary>
        /// Создание экзепляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представление главного меню приложения</param>
        public MainPresenter(ApplicationController controller, IMainView view) :
            base(controller, view)
        {
            view.GoToSubjectsView += GoToSubjectsView;
            view.GoToTestResultsView += GoToTestResultsView;
            view.UpdateTests += UpdateTests;
        }

        /// <summary>
        /// Обработчик выбора пункта перехода к списку учебных предметов
        /// </summary>
        private void GoToSubjectsView() =>
            controller.CreatePresenter<SubjectsPresenter>().Run();

        /// <summary>
        /// Обработчик выбора пункта перехода к списку результатов тестирования
        /// </summary>
        private void GoToTestResultsView() =>
            controller.CreatePresenter<TestResultsPresenter>().Run();

        /// <summary>
        /// Обработчик выбора пункта обновления базы данных тестов
        /// </summary>
        private void UpdateTests() =>
            controller.CreatePresenter<PreloadPresenter, bool>().Run(true);
    }
}