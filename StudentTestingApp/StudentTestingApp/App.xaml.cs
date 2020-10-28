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
            var controller = new ApplicationController();
            controller.RegisterSingleton<CloudStorage>();
            controller.RegisterSingleton<DbInfo>();
            controller.RegisterSingleton<ITestsLoader, TestsLoader>();
            controller.RegisterSingleton<DbHelper>();
            controller.Register<IReadOnlyRepository<Subject>, ReadOnlyRepository<Subject>>();
            controller.Register<IReadOnlyRepository<Test>, ReadOnlyRepository<Test>>();
            controller.Register<IReadOnlyRepository<Question>, ReadOnlyRepository<Question>>();
            controller.Register<IReadOnlyRepository<Answer>, ReadOnlyRepository<Answer>>();
            controller.Register<IRepository<TestResult>, Repository<TestResult>>();
            controller.Register<IPreloadView, PreloadPage>();
            controller.Register<ISubjectsView, SubjectsPage>();
            controller.Register<ITestsView, TestsPage>();
            controller.Register<ITestStartView, TestStartPage>();
            controller.Register<ITestNavigationView, TestNavigationPage>();
            controller.Register<IQuestionView, QuestionPage>();
            controller.Register<ITestResultView, TestResultPage>();
            controller.Register<ISettingsView, SettingsPage>();
            controller.Register<IMainView, MainPage>();
            controller.CreatePresenter<PreloadPresenter>().Run();
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