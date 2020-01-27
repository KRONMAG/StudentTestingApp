using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    class MainPresenter : IPresenter
    {
        private readonly IMainView _mainView;

        public MainPresenter(IMainView mainView) =>
            _mainView = mainView;

        public void Run()
        {
            _mainView.SubjectListViewSelected += SubjectListViewSelected;
            _mainView.SettingsViewSelected += SettingsViewSelected;
            _mainView.Show();
        }

        private void SubjectListViewSelected() =>
            ApplicationController.Instance.CreatePresenter<SubjectListPresenter>().Run();

        private void SettingsViewSelected() =>
            ApplicationController.Instance.CreatePresenter<SettingsPresenter>().Run();
    }
}