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
        public MainPresenter
            (ApplicationController controller,
            IMainView view) :
            base(controller, view)
        {
            view.GoToSubjectsView += GoToSubjectsView;
            view.GoToTestResultsView += GoToTestResultsView;
            view.GoToMarksView += GoToMarksView;
            view.GoToSettingsView += GoToSettingsView;
        }

        /// <summary>
        /// Обработчик запроса перехода к списку учебных предметов
        /// </summary>
        private void GoToSubjectsView() =>
            controller.CreatePresenter<SubjectsPresenter>().Run();

        /// <summary>
        /// Обработчик запроса перехода к списку результатов тестирования
        /// </summary>
        private void GoToTestResultsView() =>
            controller.CreatePresenter<TestResultsPresenter>().Run();

        /// <summary>
        /// Обработчик запроса перехода к представлению оценок
        /// </summary>
        private void GoToMarksView() =>
            controller.CreatePresenter<MarksStatisticsPresenter>().Run();

        /// <summary>
        /// Обработчик запроса перехода к настройкам приложения
        /// </summary>
        private void GoToSettingsView() =>
            controller.CreatePresenter<SettingsPresenter>().Run();
    }
}