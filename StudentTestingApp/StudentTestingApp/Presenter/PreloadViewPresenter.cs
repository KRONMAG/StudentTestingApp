using System;
using System.Threading.Tasks;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Model;
using Unity;

namespace StudentTestingApp.Presenter
{
    class PreloadViewPresenter
    {
        private IPreloadView preloadView;

        public PreloadViewPresenter(IPreloadView preloadView)
        {
            this.preloadView = preloadView;
            preloadView.Show();
            new Task(() =>
            {
                if (!DB.Instance.InitializeDB()) preloadView.ShowError("Не удалось загрузить тесты, проверьте наличие интернет-соединения");
                new SubjectViewPresenter(App.Container.Resolve<ISubjectListView>());
            }).Start();
        }
    }
}