using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class MainPresenter : BasePresenter<IMainView>
    {
        public MainPresenter(ApplicationController controller, IMainView view) :
            base(controller, view)
        {
            view.SubjectsViewSelected += SubjectsViewSelected;
            view.TestResultsViewSelected += TestResultsViewSelected;
            view.UpdateTestsSelected += UpdateTestsSelected;
        }

        private void SubjectsViewSelected() =>
            controller.CreatePresenter<SubjectsPresenter>().Run();

        private void TestResultsViewSelected() =>
            controller.CreatePresenter<TestResultsPresenter>().Run();

        private void UpdateTestsSelected() =>
            controller.CreatePresenter<PreloadPresenter, bool>().Run(true);
    }
}