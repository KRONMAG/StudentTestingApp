using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model.DataAccess;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter;
using StudentTestingApp.View;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Presenter.Common;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace StudentTestingApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            new ApplicationController()
                .RegisterSingleton<CloudStorage>()
                .RegisterSingleton<DbInfo>()
                .RegisterSingleton<ITestsLoader, TestsLoader>()
                .RegisterSingleton<DbHelper>()
                .Register<IReadOnlyRepository<Subject>, ReadOnlyRepository<Subject>>()
                .Register<IReadOnlyRepository<Test>, ReadOnlyRepository<Test>>()
                .Register<IReadOnlyRepository<Question>, ReadOnlyRepository<Question>>()
                .Register<IReadOnlyRepository<Answer>, ReadOnlyRepository<Answer>>()
                .Register<IRepository<TestResult>, Repository<TestResult>>()
                .Register<IPreloadView, PreloadPage>()
                .Register<ISubjectsView, SubjectsPage>()
                .Register<ITestsView, TestsPage>()
                .Register<ITestStartView, TestStartPage>()
                .Register<ITestNavigationView, TestNavigationPage>()
                .Register<IQuestionView, QuestionPage>()
                .Register<ITestResultView, TestResultPage>()
                .Register<ITestResultsView, TestResultsPage>()
                .Register<IMainView, MainPage>()
                .CreatePresenter<PreloadPresenter, bool>()
                .Run(false);
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}