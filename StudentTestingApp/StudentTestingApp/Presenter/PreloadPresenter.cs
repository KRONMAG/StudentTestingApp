using System.Threading.Tasks;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class PreloadPresenter : IPresenter
    {
        private readonly IPreloadView _preloadView;
        private readonly IDbLoader _dbLoader;

        public PreloadPresenter(IPreloadView preloadView, IDbLoader dbLoader)
        {
            _preloadView = preloadView;
            _dbLoader = dbLoader;
        }

        public void Run()
        {
            _preloadView.Show();
            new Task(() =>
            {
                if (!_dbLoader.DbExist && !_dbLoader.LoadDb())
                {
                    _dbLoader.CreateEmptyDb();
                    _preloadView.ShowError("Не удалось загрузить тесты, проверьте наличие интернет-соединения");
                }

                _preloadView.Close();
                ApplicationController.Instance.Run<SubjectListPresenter>();
            }).Start();
        }
    }
}