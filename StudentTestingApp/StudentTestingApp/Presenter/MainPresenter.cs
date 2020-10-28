using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    class MainPresenter : BasePresenter<IMainView>
    {
        public MainPresenter(ApplicationController controller, IMainView view) :
            base(controller, view)
        {
            view.SubjectListViewSelected += SubjectListViewSelected;
            view.SettingsViewSelected += SettingsViewSelected;
        }

        private void SubjectListViewSelected() =>
            controller.CreatePresenter<SubjectsPresenter>().Run();

        private void SettingsViewSelected() =>
            controller.CreatePresenter<SettingsPresenter>().Run();
    }
}