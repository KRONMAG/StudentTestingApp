using System;
using System.Threading.Tasks;
using StudentTestingApp.Model;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Interface;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class PreloadPresenter : IPresenter
    {
        private IPreloadView preloadView;

        public PreloadPresenter(IPreloadView preloadView)
        {
            this.preloadView = preloadView;
        }

        public void Run()
        {
            preloadView.Show();
            new Task(() =>
            {
                if (!DB.Instance.InitializeDB())
                    preloadView.ShowError("Не удалось загрузить тесты, проверьте наличие интернет-соединения");
                new SubjectPresenter(App.Container.Resolve<ISubjectListView>()).Run();
            }).Start();
        }
    }
}