using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    class MainPresenter : IPresenter
    {
        private IMainView _mainView;

        public MainPresenter(IMainView mainView)
        {
            _mainView = mainView;
        }

        public void Run()
        {
            _mainView.SubjectListViewSelected += SubjectListViewSelected;
            _mainView.Show();
        }

        private void SubjectListViewSelected()
        {
            _mainView.Close();
            ApplicationController.Instance.CreatePresenter<SubjectListPresenter>().Run();
        }
    }
}